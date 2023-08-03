namespace Domain.UseCases.Interfaces.Auth
{
    public interface IForgotPassword
    {
        Task<string> ExecuteAsync(string? email);
    }
}