using System;
namespace CopetSystem.Application.DTOs.Auth
{
	public class UserResetPasswordDTO
	{
		public Guid? Id { get; set; }
		public string? Password { get; set; }
    }
}

