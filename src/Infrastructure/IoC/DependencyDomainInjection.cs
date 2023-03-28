using Domain.Interfaces.UseCases.Area;
using Domain.Interfaces.UseCases.Auth;
using Domain.Interfaces.UseCases.MainArea;
using Domain.Interfaces.UseCases.SubArea;
using Domain.Mappings;
using Domain.UseCases.Area;
using Domain.UseCases.Auth;
using Domain.UseCases.MainArea;
using Domain.UseCases.SubArea;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC;
public static class DependencyDomainInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        #region UseCases
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
        #endregion

        #region Contract Mappers
        services.AddAutoMapper(typeof(AreaMappings));
        services.AddAutoMapper(typeof(AuthMappings));
        services.AddAutoMapper(typeof(MainAreaMappings));
        services.AddAutoMapper(typeof(SubAreaMappings));
        services.AddAutoMapper(typeof(UserMappings));
        #endregion

        return services;
    }
}