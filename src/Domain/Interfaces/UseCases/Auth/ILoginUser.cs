using Domain.Contracts.Auth;

namespace Domain.Interfaces.UseCases.Auth;
public interface ILoginUser
{
    Task<UserLoginOutput> Execute(UserLoginInput dto);
}