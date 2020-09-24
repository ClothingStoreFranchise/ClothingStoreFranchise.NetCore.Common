using Jose;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Common.Security
{
    public class CustomAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        public CustomAuthenticationHandler(
            IOptionsMonitor<BasicAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Unauthorized");

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.NoResult();
            }

            if (!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            string token = authorizationHeader.Substring("bearer".Length).Trim();

            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            try
            {
                return validateToken(token);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
        }

        private AuthenticateResult validateToken(string token)
        {
            var headers = JWT.Headers(token);
            var jwk = headers.FirstOrDefault(t => t.Key == "jwk").Value;
            var jwkDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(jwk));
            var e = jwkDictionary.First(x => x.Key == "e").Value;
            var n = jwkDictionary.First(x => x.Key == "n").Value;


            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            key.ImportParameters(new RSAParameters
            {
                Modulus = Base64Url.Decode(n),
                Exponent = Base64Url.Decode(e)
            });

            var paylod = JWT.Decode(token, key);
            var json = JObject.Parse(paylod);

            var rolJson = json.Value<JArray>("authorities").ToString();
            var rol = JsonConvert.DeserializeObject<string[]>(rolJson);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, json.Value<string>("sub").ToString()),
                new Claim(ClaimTypes.Expiration, json.Value<string>("exp").ToString()),
                new Claim(ClaimTypes.Expired, json.Value<string>("iat").ToString()),
                new Claim(ClaimTypes.NameIdentifier, json.Value<string>("userId").ToString()),
                new Claim(ClaimTypes.Role, rolJson),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new System.Security.Principal.GenericPrincipal(identity, rol);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
