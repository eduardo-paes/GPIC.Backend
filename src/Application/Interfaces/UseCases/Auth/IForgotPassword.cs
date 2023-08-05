namespace Application.Interfaces.UseCases.Auth
{
    public interface IForgotPassword
    {
        Task<string> ExecuteAsync(string? email);
    }
}