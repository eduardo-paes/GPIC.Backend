using System.ComponentModel.DataAnnotations;

namespace Application.Ports.Auth
{
    public class UserLoginInput
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}