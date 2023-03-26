using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Auth
{
    public class UserRegisterInput
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Role { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? CPF { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}

