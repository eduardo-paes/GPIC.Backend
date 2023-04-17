using Domain.Interfaces.Services;
using Infrastructure.Services.Email;
using Infrastructure.Services.Email.Configs;

namespace Services.Email
{
    public class EmailServiceFactory : IEmailServiceFactory
    {
        public IEmailService Create(EmailConfiguration configuration)
        {
            string? from = configuration.From;
            string? smtpServer = configuration.SmtpServer;
            int smtpPort = configuration.SmtpPort;
            string? smtpUsername = configuration.SmtpUsername;
            string? smtpPassword = configuration.SmtpPassword;

            return new EmailService(from, smtpServer, smtpPort, smtpUsername, smtpPassword);
        }
    }
}