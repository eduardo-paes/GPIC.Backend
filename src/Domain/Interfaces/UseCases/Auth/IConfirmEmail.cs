namespace Domain.Interfaces.UseCases;
public interface IConfirmEmail
{
    Task<string> Execute(Guid? userId, string? token);
}