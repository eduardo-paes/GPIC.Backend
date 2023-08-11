using Domain.Interfaces.Services;
using Services.Email.Configs;
using Microsoft.Extensions.Configuration;

namespace Services.Email.Factories
{
    public class EmailServiceFactory : IEmailServiceFactory
    {
        public IEmailService Create(SmtpConfiguration settings, IConfiguration configuration)
        {
            return new EmailService(
            settings.Server,
            settings.Port,
            settings.Username,
            settings.Password,
            configuration);
        }
    }
}