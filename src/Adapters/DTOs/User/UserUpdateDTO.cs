using Adapters.DTOs.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Adapters.DTOs.User
{
    public class UserUpdateDTO : RequestDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Role { get; set; }
        [Required]
        public string? CPF { get; set; }
    }
}