using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Auth
{
    public class UserLoginInput
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}