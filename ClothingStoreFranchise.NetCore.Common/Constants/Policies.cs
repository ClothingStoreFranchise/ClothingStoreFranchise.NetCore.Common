using Microsoft.AspNetCore.Authorization;

namespace ClothingStoreFranchise.NetCore.Common.Constants
{
    public class Policies
    {
        public const string Admin = "ROLE_ADMIN";
        public const string Customer = "ROLE_CUSTOMER";
        public const string ShopEmployee = "ROLE_SHOPEMPLOYEE";

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }

        public static AuthorizationPolicy CustomerPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Customer).Build();
        }

        public static AuthorizationPolicy CustomerAdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Customer, Admin).Build();
        }

        public static AuthorizationPolicy AdminShopEmployeePolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin, ShopEmployee).Build();
        }
    }
}
