namespace Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task<bool> SendConfirmationEmail(string? email, string? name, string? token);
        Task<bool> SendResetPasswordEmail(string? email, string? name, string? token);
    }
}