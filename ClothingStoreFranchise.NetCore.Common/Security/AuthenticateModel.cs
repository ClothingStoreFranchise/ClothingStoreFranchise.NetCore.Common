using System.ComponentModel.DataAnnotations;

namespace ClothingStoreFranchise.NetCore.Common.Security
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
