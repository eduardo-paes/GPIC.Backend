using Adapters.Proxies.Area;
using Adapters.Proxies.Auth;
using Adapters.Proxies.MainArea;
using Adapters.Proxies.SubArea;
using Adapters.UseCases.Area;
using Adapters.UseCases.Auth;
using Adapters.UseCases.MainArea;
using Adapters.UseCases.SubArea;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC;
public static class DependencyInjectionUseCases
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        #region Area
        services.AddScoped<ICreateArea, CreateArea>();
        services.AddScoped<IDeleteArea, DeleteArea>();
        services.AddScoped<IGetAreaById, GetAreaById>();
        services.AddScoped<IGetAreasByMainArea, GetAreasByMainArea>();
        services.AddScoped<IUpdateArea, UpdateArea>();
        #endregion

        #region Auth
        services.AddScoped<ILoginUser, LoginUser>();
        services.AddScoped<IRegisterUser, RegisterUser>();
        services.AddScoped<IResetPasswordUser, ResetPasswordUser>();
        #endregion

        #region MainArea
        services.AddScoped<ICreateMainArea, CreateMainArea>();
        services.AddScoped<IDeleteMainArea, DeleteMainArea>();
        services.AddScoped<IGetMainAreaById, GetMainAreaById>();
        services.AddScoped<IGetMainAreas, GetMainAreas>();
        services.AddScoped<IUpdateMainArea, UpdateMainArea>();
        #endregion

        #region SubArea
        services.AddScoped<ICreateSubArea, CreateSubArea>();
        services.AddScoped<IDeleteSubArea, DeleteSubArea>();
        services.AddScoped<IGetSubAreaById, GetSubAreaById>();
        services.AddScoped<IGetSubAreasByArea, GetSubAreasByArea>();
        services.AddScoped<IUpdateSubArea, UpdateSubArea>();
        #endregion

        return services;
    }
}