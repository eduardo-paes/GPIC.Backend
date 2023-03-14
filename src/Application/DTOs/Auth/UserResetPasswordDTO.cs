using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth
{
    public class UserResetPasswordDTO
    {
        [Required]
        public Guid? Id { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}

