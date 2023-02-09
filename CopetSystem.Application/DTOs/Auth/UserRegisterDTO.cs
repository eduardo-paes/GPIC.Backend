using System;
namespace CopetSystem.Application.DTOs.Auth
{
	public class UserRegisterDTO
    {
        public string? Name { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
        public string? CPF { get; set; }
        public string? Password { get; set; }
    }
}

