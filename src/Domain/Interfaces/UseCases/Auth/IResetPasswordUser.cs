using Domain.Contracts.Auth;

namespace Domain.Interfaces.UseCases.Auth;
public interface IResetPasswordUser
{
    Task<string> Execute(UserResetPasswordInput dto);
}