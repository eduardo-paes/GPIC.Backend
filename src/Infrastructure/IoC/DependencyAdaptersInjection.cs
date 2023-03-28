using Adapters.Mappings;
using Adapters.Proxies.Area;
using Adapters.Proxies.MainArea;
using Adapters.Proxies.SubArea;
using Adapters.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC;
public static class DependencyAdaptersInjection
{
    public static IServiceCollection AddAdapters(this IServiceCollection services)
    {
        #region Services
        services.AddScoped<IAreaService, AreaService>();
        services.AddScoped<IMainAreaService, MainAreaService>();
        services.AddScoped<ISubAreaService, SubAreaService>();
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