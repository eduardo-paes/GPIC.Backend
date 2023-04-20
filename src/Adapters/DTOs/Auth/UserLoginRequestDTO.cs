using Adapters.DTOs.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Adapters.DTOs.Auth
{
    public class UserLoginRequestDTO : RequestDTO
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}