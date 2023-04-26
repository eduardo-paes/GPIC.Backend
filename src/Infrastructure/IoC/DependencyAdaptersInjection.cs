using Adapters.Mappings;
using Adapters.Proxies.Area;
using Adapters.Proxies.Auth;
using Adapters.Proxies.Campus;
using Adapters.Proxies.Course;
using Adapters.Proxies.MainArea;
using Adapters.Proxies.Notice;
using Adapters.Proxies.ProgramType;
using Adapters.Proxies.Student;
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
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICampusService, CampusService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IMainAreaService, MainAreaService>();
        services.AddScoped<INoticeService, NoticeService>();
        services.AddScoped<IProgramTypeService, ProgramTypeService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ISubAreaService, SubAreaService>();
        #endregion

        #region DTO Mappers
        services.AddAutoMapper(typeof(AreaMappings));
        services.AddAutoMapper(typeof(AuthMappings));
        services.AddAutoMapper(typeof(CampusMappings));
        services.AddAutoMapper(typeof(CourseMappings));
        services.AddAutoMapper(typeof(MainAreaMappings));
        services.AddAutoMapper(typeof(NoticeMappings));
        services.AddAutoMapper(typeof(ProgramTypeMappings));
        services.AddAutoMapper(typeof(StudentMappings));
        services.AddAutoMapper(typeof(SubAreaMappings));
        services.AddAutoMapper(typeof(UserMappings));
        #endregion

        return services;
    }
}