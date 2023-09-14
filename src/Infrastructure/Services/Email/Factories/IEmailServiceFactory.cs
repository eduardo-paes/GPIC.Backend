using Domain.Interfaces.Services;
using Services.Email.Configs;

namespace Services.Email.Factories
{
    public interface IEmailServiceFactory
    {
        IEmailService Create(SmtpConfiguration settings, string frontEndUrl);
    }
}