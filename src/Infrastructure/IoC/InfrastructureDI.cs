using AspNetCoreRateLimit;
using Infrastructure.IoC.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Services;

namespace Infrastructure.IoC
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, HostBuilderContext? hostContext = null)
        {
            #region AppSettings e DotEnv
            // Define valores das propriedades de configuração
            IConfiguration configuration = SettingsConfiguration.GetConfiguration(hostContext);
            services.AddSingleton(configuration);

            // Carrega informações de ambiente (.env)
            DotEnvSecrets dotEnvSecrets = new();
            services.AddScoped<IDotEnvSecrets, DotEnvSecrets>();
            #endregion AppSettings e DotEnv

            #region Serviço de Log
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Seq(dotEnvSecrets.GetSeqUrl(), apiKey: dotEnvSecrets.GetSeqApiKey())
                .CreateLogger();
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(Log.Logger, dispose: true);
            });
            #endregion Serviço de Log

            #region CORS
            // TODO: Definir política de CORS
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
            #endregion CORS

            #region Rate Limit
#if DEBUG
            services.AddMemoryCache();
            services.AddInMemoryRateLimiting();
            services.Configure<ClientRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
            services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
#endif
            #endregion Rate Limit

            return services;
        }
    }
}