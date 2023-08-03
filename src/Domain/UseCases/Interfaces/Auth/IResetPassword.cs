using Domain.UseCases.Ports.Auth;

namespace Domain.UseCases.Interfaces.Auth
{
    public interface IResetPassword
    {
        Task<string> ExecuteAsync(UserResetPasswordInput input);
    }
}