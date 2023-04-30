using Adapters.DTOs.Auth;

namespace Adapters.Proxies;
public interface IAuthService
{
    Task<string> ConfirmEmail(Guid? userId, string? token);
    Task<string> ForgotPassword(string? email);
    Task<UserLoginResponseDTO> Login(UserLoginRequestDTO dto);
    Task<string> ResetPassword(UserResetPasswordDTO dto);
}