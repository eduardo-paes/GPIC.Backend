namespace Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string? email, string? name, string? token);
        Task SendResetPasswordEmailAsync(string? email, string? name, string? token);
        Task SendNoticeEmailAsync(string? email, string? name, DateTime? registrationStartDate, DateTime? registrationEndDate, string? noticeUrl);
        Task SendProjectNotificationEmailAsync(string? email, string? name, string? projectTitle, string? status, string? description);
        Task SendRequestStudentRegisterEmailAsync(string? email);
    }
}