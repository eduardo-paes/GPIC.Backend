using Application.Ports.Auth;

namespace Application.Interfaces.UseCases.Auth
{
    public interface ILogin
    {
        Task<UserLoginOutput> ExecuteAsync(UserLoginInput input);
    }
}