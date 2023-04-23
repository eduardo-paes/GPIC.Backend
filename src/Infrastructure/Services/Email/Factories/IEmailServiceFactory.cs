using Domain.Interfaces.Services;
using Infrastructure.Services.Email.Configs;

namespace Infrastructure.Services.Email.Factories
{
    public interface IEmailServiceFactory
    {
        IEmailService Create(SmtpConfiguration configuration);
    }
}