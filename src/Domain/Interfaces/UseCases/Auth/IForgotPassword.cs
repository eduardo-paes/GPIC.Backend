namespace Domain.Interfaces.UseCases;
public interface IForgotPassword
{
    Task<string> Execute(string? email);
}