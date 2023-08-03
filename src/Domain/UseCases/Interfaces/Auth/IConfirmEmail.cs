namespace Domain.UseCases.Interfaces.Auth
{
    public interface IConfirmEmail
    {
        Task<string> ExecuteAsync(string? email, string? token);
    }
}