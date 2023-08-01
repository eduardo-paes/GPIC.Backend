namespace Domain.UseCases.Interfaces.Auth
{
    public interface IForgotPassword
    {
        Task<string> Execute(string? email);
    }
}