using Domain.Interfaces.Services;
using Services.Email.Configs;

namespace Services.Email.Factories
{
    public class EmailServiceFactory : IEmailServiceFactory
    {
        public IEmailService Create(SmtpConfiguration settings, string frontEndUrl)
        {
            return new EmailService(
            settings.Server,
            settings.Port,
            settings.Username,
            settings.Password,
            frontEndUrl);
        }
    }
}