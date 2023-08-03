using Adapters.Interfaces;
using Adapters.Mappings;
using Adapters.PresenterController;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public static class DependencyAdaptersInjection
    {
        public static IServiceCollection AddAdapters(this IServiceCollection services)
        {
            #region PresenterControllers
            _ = services.AddScoped<IAreaPresenterController, AreaPresenterController>();
            _ = services.AddScoped<IActivityPresenterController, ActivityPresenterController>();
            _ = services.AddScoped<IAssistanceTypePresenterController, AssistanceTypePresenterController>();
            _ = services.AddScoped<IAuthPresenterController, AuthPresenterController>();
            _ = services.AddScoped<ICampusPresenterController, CampusPresenterController>();
            _ = services.AddScoped<ICoursePresenterController, CoursePresenterController>();
            _ = services.AddScoped<IMainAreaPresenterController, MainAreaPresenterController>();
            _ = services.AddScoped<INoticePresenterController, NoticePresenterController>();
            _ = services.AddScoped<IProfessorPresenterController, ProfessorPresenterController>();
            _ = services.AddScoped<IProgramTypePresenterController, ProgramTypePresenterController>();
            _ = services.AddScoped<IProjectEvaluationPresenterController, ProjectEvaluationPresenterController>();
            _ = services.AddScoped<IProjectPresenterController, ProjectPresenterController>();
            _ = services.AddScoped<IStudentDocumentsPresenterController, StudentDocumentsPresenterController>();
            _ = services.AddScoped<IStudentPresenterController, StudentPresenterController>();
            _ = services.AddScoped<ISubAreaPresenterController, SubAreaPresenterController>();
            _ = services.AddScoped<IUserPresenterController, UserPresenterController>();
            #endregion PresenterControllers

            #region Gateways Mappers
            _ = services.AddAutoMapper(typeof(AreaMappings));
            _ = services.AddAutoMapper(typeof(ActivityMappings));
            _ = services.AddAutoMapper(typeof(AssistanceTypeMappings));
            _ = services.AddAutoMapper(typeof(AuthMappings));
            _ = services.AddAutoMapper(typeof(CampusMappings));
            _ = services.AddAutoMapper(typeof(CourseMappings));
            _ = services.AddAutoMapper(typeof(MainAreaMappings));
            _ = services.AddAutoMapper(typeof(NoticeMappings));
            _ = services.AddAutoMapper(typeof(ProfessorMappings));
            _ = services.AddAutoMapper(typeof(ProgramTypeMappings));
            _ = services.AddAutoMapper(typeof(ProjectEvaluationMapping));
            _ = services.AddAutoMapper(typeof(ProjectMappings));
            _ = services.AddAutoMapper(typeof(ProjectActivityMappings));
            _ = services.AddAutoMapper(typeof(StudentDocumentsMappings));
            _ = services.AddAutoMapper(typeof(StudentMappings));
            _ = services.AddAutoMapper(typeof(SubAreaMappings));
            _ = services.AddAutoMapper(typeof(UserMappings));
            #endregion Gateways Mappers

            return services;
        }
    }
}