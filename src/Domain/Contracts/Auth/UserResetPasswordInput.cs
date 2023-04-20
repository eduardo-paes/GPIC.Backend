using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Auth
{
    public class UserResetPasswordInput
    {
        [Required]
        public Guid? Id { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}