using Domain.Interfaces.Services;
using Services.Email.Configs;

namespace Services.Email.Factories
{
    public class EmailServiceFactory : IEmailServiceFactory
    {
        public IEmailService Create(SmtpConfiguration configuration)
        {
            return new EmailService(
            configuration.Server,
            configuration.Port,
            configuration.Username,
            configuration.Password);
        }
    }
}