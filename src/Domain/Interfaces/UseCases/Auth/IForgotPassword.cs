namespace Domain.Interfaces.UseCases.Auth;
public interface IForgotPassword
{
    Task<string> Execute(string? email);
}