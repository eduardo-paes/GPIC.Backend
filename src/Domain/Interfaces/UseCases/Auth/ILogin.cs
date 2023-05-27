using Domain.Contracts.Auth;

namespace Domain.Interfaces.UseCases;
public interface ILogin
{
    Task<UserLoginOutput> Execute(UserLoginInput input);
}