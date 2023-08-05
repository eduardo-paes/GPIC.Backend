using System.ComponentModel.DataAnnotations;

namespace Application.Ports.User
{
    public class UserUpdateInput
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? CPF { get; set; }
    }
}