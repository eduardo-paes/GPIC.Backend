using Domain.Contracts.Auth;

namespace Domain.Interfaces.UseCases.Auth;
public interface ILogin
{
    Task<UserLoginOutput> Execute(UserLoginInput dto);
}