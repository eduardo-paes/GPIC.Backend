namespace Domain.Interfaces.UseCases.Auth;
public interface IConfirmEmail
{
    Task<string> Execute(Guid? userId, string? token);
}