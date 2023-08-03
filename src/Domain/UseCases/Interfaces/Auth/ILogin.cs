using Domain.UseCases.Ports.Auth;

namespace Domain.UseCases.Interfaces.Auth
{
    public interface ILogin
    {
        Task<UserLoginOutput> ExecuteAsync(UserLoginInput input);
    }
}