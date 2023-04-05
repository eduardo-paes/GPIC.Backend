using Domain.Interfaces.Services;
using Domain.Interfaces.UseCases.Area;
using Domain.Interfaces.UseCases.Auth;
using Domain.Interfaces.UseCases.Campus;
using Domain.Interfaces.UseCases.Course;
using Domain.Interfaces.UseCases.MainArea;
using Domain.Interfaces.UseCases.Notice;
using Domain.Interfaces.UseCases.ProgramType;
using Domain.Interfaces.UseCases.SubArea;
using Domain.Mappings;
using Domain.UseCases.Area;
using Domain.UseCases.Auth;
using Domain.UseCases.Campus;
using Domain.UseCases.Course;
using Domain.UseCases.MainArea;
using Domain.UseCases.Notice;
using Domain.UseCases.ProgramType;
using Domain.UseCases.SubArea;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC;
public static class DependencyDomainInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        #region Services
        services.AddScoped<ITokenHandler, TokenHandler>();
        services.AddScoped<IStorageFileService, StorageFileService>();
        #endregion

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

        #region Campus
        services.AddScoped<ICreateCampus, CreateCampus>();
        services.AddScoped<IDeleteCampus, DeleteCampus>();
        services.AddScoped<IGetCampusById, GetCampusById>();
        services.AddScoped<IGetCampuses, GetCampuses>();
        services.AddScoped<IUpdateCampus, UpdateCampus>();
        #endregion

        #region Course
        services.AddScoped<ICreateCourse, CreateCourse>();
        services.AddScoped<IDeleteCourse, DeleteCourse>();
        services.AddScoped<IGetCourseById, GetCourseById>();
        services.AddScoped<IGetCourses, GetCourses>();
        services.AddScoped<IUpdateCourse, UpdateCourse>();
        #endregion

        #region MainArea
        services.AddScoped<ICreateMainArea, CreateMainArea>();
        services.AddScoped<IDeleteMainArea, DeleteMainArea>();
        services.AddScoped<IGetMainAreaById, GetMainAreaById>();
        services.AddScoped<IGetMainAreas, GetMainAreas>();
        services.AddScoped<IUpdateMainArea, UpdateMainArea>();
        #endregion

        #region Notice
        services.AddScoped<ICreateNotice, CreateNotice>();
        services.AddScoped<IDeleteNotice, DeleteNotice>();
        services.AddScoped<IGetNoticeById, GetNoticeById>();
        services.AddScoped<IGetNotices, GetNotices>();
        services.AddScoped<IUpdateNotice, UpdateNotice>();
        #endregion

        #region ProgramType
        services.AddScoped<ICreateProgramType, CreateProgramType>();
        services.AddScoped<IDeleteProgramType, DeleteProgramType>();
        services.AddScoped<IGetProgramTypeById, GetProgramTypeById>();
        services.AddScoped<IGetProgramTypes, GetProgramTypes>();
        services.AddScoped<IUpdateProgramType, UpdateProgramType>();
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
        services.AddAutoMapper(typeof(CampusMappings));
        services.AddAutoMapper(typeof(CourseMappings));
        services.AddAutoMapper(typeof(MainAreaMappings));
        services.AddAutoMapper(typeof(NoticeMappings));
        services.AddAutoMapper(typeof(ProgramTypeMappings));
        services.AddAutoMapper(typeof(SubAreaMappings));
        services.AddAutoMapper(typeof(UserMappings));
        #endregion

        return services;
    }
}