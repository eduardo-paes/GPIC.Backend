using Application.DTOs.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth
{
    public class UserResetPasswordDTO : RequestDTO
    {
        [Required]
        public Guid? Id { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}

