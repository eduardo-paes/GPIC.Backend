using Adapters.Mappings;
using Adapters.Interfaces;
using Adapters.PresenterController;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC;
public static class DependencyAdaptersInjection
{
    public static IServiceCollection AddAdapters(this IServiceCollection services)
    {
        #region PresenterControllers
        services.AddScoped<IAreaPresenterController, AreaPresenterController>();
        services.AddScoped<IAuthPresenterController, AuthPresenterController>();
        services.AddScoped<ICampusPresenterController, CampusPresenterController>();
        services.AddScoped<ICoursePresenterController, CoursePresenterController>();
        services.AddScoped<IMainAreaPresenterController, MainAreaPresenterController>();
        services.AddScoped<INoticePresenterController, NoticePresenterController>();
        services.AddScoped<IProfessorPresenterController, ProfessorPresenterController>();
        services.AddScoped<IProgramTypePresenterController, ProgramTypePresenterController>();
        services.AddScoped<IStudentPresenterController, StudentPresenterController>();
        services.AddScoped<ISubAreaPresenterController, SubAreaPresenterController>();
        services.AddScoped<IUserPresenterController, UserPresenterController>();
        #endregion

        #region Gateways Mappers
        services.AddAutoMapper(typeof(AreaMappings));
        services.AddAutoMapper(typeof(AuthMappings));
        services.AddAutoMapper(typeof(CampusMappings));
        services.AddAutoMapper(typeof(CourseMappings));
        services.AddAutoMapper(typeof(MainAreaMappings));
        services.AddAutoMapper(typeof(NoticeMappings));
        services.AddAutoMapper(typeof(ProfessorMappings));
        services.AddAutoMapper(typeof(ProgramTypeMappings));
        services.AddAutoMapper(typeof(StudentMappings));
        services.AddAutoMapper(typeof(SubAreaMappings));
        services.AddAutoMapper(typeof(UserMappings));
        #endregion

        return services;
    }
}