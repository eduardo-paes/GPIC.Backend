using Application.DTOs.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.User
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