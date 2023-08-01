using System.ComponentModel.DataAnnotations;

namespace Domain.UseCases.Ports.Auth
{
    public class UserResetPasswordInput
    {
        [Required]
        public Guid? Id { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Token { get; set; }
    }
}