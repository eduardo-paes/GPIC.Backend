using Domain.Interfaces.Services;
using Infrastructure.IoC.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Email.Configs;
using Services.Email.Factories;

namespace Infrastructure.IoC
{
    public static class ExternalServicesDI
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            // Define valores das propriedades de configuração
            IConfiguration configuration = SettingsConfiguration.GetConfiguration();

            // Carrega informações de ambiente (.env)
            DotEnvSecrets dotEnvSecrets = new();

            #region Serviço de E-mail
            SmtpConfiguration smtpConfig = new();
            configuration.GetSection("SmtpConfiguration").Bind(smtpConfig);
            smtpConfig.Password = dotEnvSecrets.GetSmtpUserPassword();
            smtpConfig.Username = dotEnvSecrets.GetSmtpUserName();
            services.AddSingleton<IEmailServiceFactory, EmailServiceFactory>();
            services.AddSingleton(sp =>
            {
                IEmailServiceFactory factory = sp.GetRequiredService<IEmailServiceFactory>();
                return factory.Create(smtpConfig, configuration);
            });
            #endregion Serviço de E-mail

            #region Demais Serviços
            services.AddHttpContextAccessor();
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<ITokenAuthenticationService, TokenAuthenticationService>();
            services.AddScoped<IStorageFileService, AzureStorageService>();
            #endregion Demais Serviços

            return services;
        }
    }
}