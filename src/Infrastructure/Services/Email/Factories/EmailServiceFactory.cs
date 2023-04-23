using Domain.Interfaces.Services;
using Infrastructure.Services.Email.Configs;

namespace Infrastructure.Services.Email.Factories;
public class EmailServiceFactory : IEmailServiceFactory
{
    public IEmailService Create(SmtpConfiguration configuration) => new EmailService(
        configuration.Server,
        configuration.Port,
        configuration.Username,
        configuration.Password);
}