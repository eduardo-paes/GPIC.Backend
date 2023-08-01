namespace Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task<bool> SendConfirmationEmail(string? email, string? name, string? token);
        Task<bool> SendResetPasswordEmail(string? email, string? name, string? token);
        Task SendNoticeEmail(string? email, string? name, DateTime? registrationStartDate, DateTime? registrationEndDate, string? noticeUrl);
        Task SendProjectNotificationEmail(string? email, string? name, string? projectTitle, string? status, string? description);
    }
}