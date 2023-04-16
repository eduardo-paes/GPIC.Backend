using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.User
{
    public class UserUpdateInput
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? CPF { get; set; }
    }
}