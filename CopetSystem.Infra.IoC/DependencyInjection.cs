using CopetSystem.Application.Interfaces;
using CopetSystem.Application.Mappings;
using CopetSystem.Application.Services;
using CopetSystem.Domain.Interfaces;
using CopetSystem.Infra.Data.Context;
using CopetSystem.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CopetSystem.Infra.IoC;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            o => o.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        // Serviços de Negócios
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();

        // Repositórios
        services.AddScoped<IUserRepository, UserRepository>();

        // DTOs
        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

        return services;
    }
}

