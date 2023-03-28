using Adapters.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.DTOs.Auth
{
    public class UserResetPasswordDTO : RequestDTO
    {
        [Required]
        public Guid? Id { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}