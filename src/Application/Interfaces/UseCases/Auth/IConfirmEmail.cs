namespace Application.Interfaces.UseCases.Auth
{
    public interface IConfirmEmail
    {
        Task<string> ExecuteAsync(string? email, string? token);
    }
}