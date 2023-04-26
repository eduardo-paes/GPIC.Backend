using Domain.Contracts.Auth;

namespace Domain.Interfaces.UseCases.Auth;
public interface IResetPassword
{
    Task<string> Execute(UserResetPasswordInput dto);
}