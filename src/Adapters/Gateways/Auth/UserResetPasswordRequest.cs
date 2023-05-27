using Adapters.Gateways.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.Auth
{
    public class UserResetPasswordRequest : Request
    {
        [Required]
        public Guid? Id { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Token { get; set; }
    }
}