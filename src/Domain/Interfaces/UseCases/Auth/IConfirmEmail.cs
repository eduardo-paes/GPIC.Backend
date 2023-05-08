namespace Domain.Interfaces.UseCases;
public interface IConfirmEmail
{
    Task<string> Execute(string? email, string? token);
}