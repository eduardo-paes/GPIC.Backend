using Application.DTOs.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth
{
    public class UserLoginRequestDTO : RequestDTO
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}

