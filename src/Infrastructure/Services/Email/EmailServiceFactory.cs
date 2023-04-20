using Domain.Interfaces.Services;
using Infrastructure.Services.Email;
using Infrastructure.Services.Email.Configs;

namespace Services.Email
{
    public class EmailServiceFactory : IEmailServiceFactory
    {
        public IEmailService Create(EmailConfiguration configuration) => new EmailService(
            configuration.From,
            configuration.SmtpServer,
            configuration.SmtpPort,
            configuration.SmtpUsername,
            configuration.SmtpPassword,
            configuration.ApiDomain);
    }
}