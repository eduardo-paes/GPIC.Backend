using CopetSystem.Application.DTOs.Auth;
using CopetSystem.Application.DTOs.User;

namespace CopetSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<UserReadDTO> Register(UserRegisterDTO dto);
        Task<UserLoginResponseDTO> Login(UserLoginRequestDTO dto);
        Task<bool> ResetPassword(UserResetPasswordDTO dto);
    }
}

