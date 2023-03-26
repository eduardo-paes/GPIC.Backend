using Domain.Contracts.Auth;
using Domain.Contracts.User;

namespace Domain.Interfaces.Auth
{
    public interface IRegisterUser
    {
        Task<UserReadOutput> Execute(UserRegisterInput dto);
    }
}