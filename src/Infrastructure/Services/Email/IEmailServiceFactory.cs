using Domain.Interfaces.Services;
using Infrastructure.Services.Email.Configs;

namespace Services.Email
{
    public interface IEmailServiceFactory
    {
        IEmailService Create(EmailConfiguration configuration);
    }
}