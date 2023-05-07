using Adapters.Mappings;
using Adapters.Proxies;
using Adapters.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC;
public static class DependencyAdaptersInjection
{
    public static IServiceCollection AddAdapters(this IServiceCollection services)
    {
        #region Services
        services.AddScoped<IAreaService, AreaService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICampusService, CampusService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IMainAreaService, MainAreaService>();
        services.AddScoped<INoticeService, NoticeService>();
        services.AddScoped<IProfessorService, ProfessorService>();
        services.AddScoped<IProgramTypeService, ProgramTypeService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ISubAreaService, SubAreaService>();
        services.AddScoped<IUserService, UserService>();
        #endregion

        #region DTO Mappers
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