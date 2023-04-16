using Domain.Interfaces.Services;
using Infrastructure.Services.Email;
using Infrastructure.Services.Email.Configs;
using Microsoft.Extensions.Configuration;

namespace Services.Email
{
    public class EmailServiceFactory : IEmailServiceFactory
    {
        private readonly IConfiguration _configuration;

        public EmailServiceFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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