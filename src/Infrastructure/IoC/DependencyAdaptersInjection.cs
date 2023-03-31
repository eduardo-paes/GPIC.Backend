using Adapters.Mappings;
using Adapters.Proxies.Area;
using Adapters.Proxies.Course;
using Adapters.Proxies.MainArea;
using Adapters.Proxies.Notice;
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
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IMainAreaService, MainAreaService>();
        services.AddScoped<INoticeService, NoticeService>();
        services.AddScoped<ISubAreaService, SubAreaService>();
        #endregion

        #region DTO Mappers
        services.AddAutoMapper(typeof(AreaMappings));
        services.AddAutoMapper(typeof(AuthMappings));
        services.AddAutoMapper(typeof(CourseMappings));
        services.AddAutoMapper(typeof(MainAreaMappings));
        services.AddAutoMapper(typeof(NoticeMappings));
        services.AddAutoMapper(typeof(SubAreaMappings));
        services.AddAutoMapper(typeof(UserMappings));
        #endregion

        return services;
    }
}