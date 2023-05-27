using Adapters.Gateways.Auth;

namespace Adapters.Interfaces;
public interface IAuthPresenterController
{
    Task<string> ConfirmEmail(string? email, string? token);
    Task<string> ForgotPassword(string? email);
    Task<UserLoginResponse> Login(UserLoginRequest request);
    Task<string> ResetPassword(UserResetPasswordRequest request);
}