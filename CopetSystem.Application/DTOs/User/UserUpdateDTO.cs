using System;
using System.ComponentModel.DataAnnotations;

namespace CopetSystem.Application.DTOs.User
{
	public class UserUpdateDTO
    {
		[Required]
        public string? Name { get; set; }
		[Required]
        public string? Role { get; set; }
		[Required]
        public string? CPF { get; set; }
    }
}