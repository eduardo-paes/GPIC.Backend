using Domain.Contracts.Auth;

namespace Domain.Interfaces.Auth
{
    public interface IResetPasswordUser
    {
        Task<bool> Execute(UserResetPasswordInput dto);
    }
}