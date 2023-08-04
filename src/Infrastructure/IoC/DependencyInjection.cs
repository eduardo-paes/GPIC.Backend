using AspNetCoreRateLimit;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using IoC.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence.Repositories;
using Serilog;
using Services;
using Services.Email.Configs;
using Services.Email.Factories;

namespace IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Define valores das propriedades de configuração
            IConfiguration configuration = SettingsConfiguration.GetConfiguration();
            _ = services.AddSingleton(configuration);

            // Carrega informações de ambiente (.env)
            DotEnvSecrets dotEnvSecrets = new();
            _ = services.AddScoped<IDotEnvSecrets, DotEnvSecrets>();

            #region Inicialização do banco de dados
            _ = services.AddDbContext<ApplicationDbContext>(
                o => o.UseNpgsql(dotEnvSecrets.GetDatabaseConnectionString(),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            #endregion Inicialização do banco de dados

            #region Serviço de Log
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            _ = services.AddLogging(loggingBuilder =>
            {
                _ = loggingBuilder.ClearProviders();
                _ = loggingBuilder.AddSerilog(Log.Logger, dispose: true);
            });
            #endregion Serviço de Log

            #region CORS
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        _ = policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
            #endregion CORS

            #region Serviço de E-mail
            SmtpConfiguration smtpConfig = new();
            configuration.GetSection("SmtpConfiguration").Bind(smtpConfig);
            smtpConfig.Password = dotEnvSecrets.GetSmtpUserPassword();
            smtpConfig.Username = dotEnvSecrets.GetSmtpUserName();
            _ = services.AddSingleton<IEmailServiceFactory, EmailServiceFactory>();
            _ = services.AddSingleton(sp =>
            {
                IEmailServiceFactory factory = sp.GetRequiredService<IEmailServiceFactory>();
                return factory.Create(smtpConfig, configuration);
            });
            #endregion Serviço de E-mail

            #region Repositórios
            _ = services.AddScoped<IAreaRepository, AreaRepository>();
            _ = services.AddScoped<IActivityRepository, ActivityRepository>();
            _ = services.AddScoped<IActivityTypeRepository, ActivityTypeRepository>();
            _ = services.AddScoped<IAssistanceTypeRepository, AssistanceTypeRepository>();
            _ = services.AddScoped<ICampusRepository, CampusRepository>();
            _ = services.AddScoped<ICourseRepository, CourseRepository>();
            _ = services.AddScoped<IMainAreaRepository, MainAreaRepository>();
            _ = services.AddScoped<INoticeRepository, NoticeRepository>();
            _ = services.AddScoped<IProfessorRepository, ProfessorRepository>();
            _ = services.AddScoped<IProgramTypeRepository, ProgramTypeRepository>();
            _ = services.AddScoped<IProjectRepository, ProjectRepository>();
            _ = services.AddScoped<IProjectActivityRepository, ProjectActivityRepository>();
            _ = services.AddScoped<IProjectEvaluationRepository, ProjectEvaluationRepository>();
            _ = services.AddScoped<IStudentRepository, StudentRepository>();
            _ = services.AddScoped<IStudentDocumentsRepository, StudentDocumentsRepository>();
            _ = services.AddScoped<ISubAreaRepository, SubAreaRepository>();
            _ = services.AddScoped<IUserRepository, UserRepository>();
            #endregion Repositórios

            #region Rate Limit
            _ = services.AddMemoryCache();
            _ = services.AddInMemoryRateLimiting();
            _ = services.Configure<ClientRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
            _ = services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
            _ = services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            _ = services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            _ = services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            #endregion Rate Limit

            return services;
        }
    }
}