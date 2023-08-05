using Application.Ports.Auth;

namespace Application.Interfaces.UseCases.Auth
{
    public interface IResetPassword
    {
        Task<string> ExecuteAsync(UserResetPasswordInput input);
    }
}