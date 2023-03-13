using Application.DTOs.Auth;
using Application.DTOs.User;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<UserReadDTO> Register(UserRegisterDTO dto);
        Task<UserLoginResponseDTO> Login(UserLoginRequestDTO dto);
        Task<bool> ResetPassword(UserResetPasswordDTO dto);
    }
}

