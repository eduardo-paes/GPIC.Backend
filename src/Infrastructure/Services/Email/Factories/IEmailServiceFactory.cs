using Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Services.Email.Configs;

namespace Services.Email.Factories
{
    public interface IEmailServiceFactory
    {
        IEmailService Create(SmtpConfiguration settings, IConfiguration configuration);
    }
}