namespace Domain.Interfaces.UseCases.User;
public interface IConfirmUserEmail
{
    Task Execute(Guid? userId, string? token);
}