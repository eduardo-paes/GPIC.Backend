using System;
using System.ComponentModel.DataAnnotations;

namespace CopetSystem.Application.DTOs.Auth
{
	public class UserLoginResponseDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}

