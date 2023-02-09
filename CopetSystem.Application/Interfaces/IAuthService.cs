using System;
using CopetSystem.Application.DTOs.Auth;
using CopetSystem.Application.DTOs.User;

namespace CopetSystem.Application.Interfaces
{
	public interface IAuthService
	{
        Task<UserReadDTO> Register(UserRegisterDTO dto);
        Task<string> Login(UserLoginDTO dto);
        Task<bool> ResetPassword(UserResetPasswordDTO dto);
    }
}

