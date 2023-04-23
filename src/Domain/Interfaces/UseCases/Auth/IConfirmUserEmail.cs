namespace Domain.Interfaces.UseCases.Auth;
public interface IConfirmUserEmail
{
    Task<string> Execute(Guid? userId, string? token);
}