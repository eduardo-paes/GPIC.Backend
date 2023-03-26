using Adapters.Mappings;
using Domain.Interfaces;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Infrastructure.IoC;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        #region Acesso Appsettings
        // Adicione o caminho base para o arquivo appsettings.json
        var basePath = Path.GetDirectoryName(typeof(DependencyInjection).Assembly.Location);
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        // Use a configuração criada acima para ler as configurações do appsettings.json
        configuration = configurationBuilder.Build();
        #endregion

        #region Inicialização do banco de dados
        services.AddDbContext<AdaptersDbContext>(
            o => o.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(AdaptersDbContext).Assembly.FullName)));
        #endregion

        #region Serviços de Log 
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog(Log.Logger, dispose: true);
        });
        #endregion

        #region Services
        services.AddScoped<ITokenHandler, TokenHandler>();
        #endregion

        #region Repositórios
        services.AddScoped<IAreaRepository, AreaRepository>();
        services.AddScoped<IMainAreaRepository, MainAreaRepository>();
        services.AddScoped<ISubAreaRepository, SubAreaRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        #endregion

        #region DTO Mappers
        services.AddAutoMapper(typeof(AreaMappings));
        services.AddAutoMapper(typeof(AuthMappings));
        services.AddAutoMapper(typeof(MainAreaMappings));
        services.AddAutoMapper(typeof(SubAreaMappings));
        services.AddAutoMapper(typeof(UserMappings));
        #endregion

        return services;
    }
}

