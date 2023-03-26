using Application.DTOs.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth
{
    public class UserRegisterDTO : RequestDTO
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

