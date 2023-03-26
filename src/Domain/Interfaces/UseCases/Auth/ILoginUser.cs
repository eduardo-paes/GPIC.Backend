using Domain.Contracts.Auth;

namespace Domain.Interfaces.Auth
{
    public interface ILoginUser
    {
        Task<UserLoginOutput> Execute(UserLoginInput dto);
    }
}