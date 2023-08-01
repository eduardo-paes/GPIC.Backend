using Domain.UseCases.Ports.Auth;

namespace Domain.UseCases.Interfaces.Auth
{
    public interface IResetPassword
    {
        Task<string> Execute(UserResetPasswordInput input);
    }
}