using Domain.Contracts.Auth;

namespace Domain.Interfaces.UseCases;
public interface IResetPassword
{
    Task<string> Execute(UserResetPasswordInput dto);
}