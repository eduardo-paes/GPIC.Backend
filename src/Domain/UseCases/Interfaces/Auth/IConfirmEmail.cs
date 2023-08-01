namespace Domain.UseCases.Interfaces.Auth
{
    public interface IConfirmEmail
    {
        Task<string> Execute(string? email, string? token);
    }
}