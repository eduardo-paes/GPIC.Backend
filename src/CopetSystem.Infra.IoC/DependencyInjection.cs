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
        services.AddDbContext<ApplicationDbContext>(options =>
         options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"
        ), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        // Repositórios
        services.AddScoped<IUserRepository, UserRepository>();

        // Serviços de Negócios
        services.AddScoped<IUserService, UserService>();

        // DTOs
        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

        //var myhandlers = AppDomain.CurrentDomain.Load("CopetSystem.Application");
        //services.AddMediatR(myhandlers);

        return services;
    }
}

