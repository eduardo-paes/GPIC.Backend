using Domain.Interfaces.Services;
using Domain.Interfaces.UseCases;
using Domain.Interfaces.UseCases.Project;
using Domain.Interfaces.UseCases.ProjectEvaluation;
using Domain.Mappings;
using Domain.UseCases;
using Domain.UseCases.Project;
using Domain.UseCases.ProjectEvaluation;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC;
public static class DependencyDomainInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        #region External Services
        services.AddHttpContextAccessor();
        services.AddScoped<IHashService, HashService>();
        services.AddScoped<ITokenAuthenticationService, TokenAuthenticationService>();
        // services.AddScoped<IStorageFileService, StorageFileService>();
        services.AddScoped<IStorageFileService, AzureStorageService>();
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
        services.AddScoped<IConfirmEmail, ConfirmEmail>();
        services.AddScoped<IForgotPassword, ForgotPassword>();
        services.AddScoped<ILogin, Login>();
        services.AddScoped<IResetPassword, ResetPassword>();
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

        #region Professor
        services.AddScoped<ICreateProfessor, CreateProfessor>();
        services.AddScoped<IDeleteProfessor, DeleteProfessor>();
        services.AddScoped<IGetProfessorById, GetProfessorById>();
        services.AddScoped<IGetProfessors, GetProfessors>();
        services.AddScoped<IUpdateProfessor, UpdateProfessor>();
        #endregion

        #region ProgramType
        services.AddScoped<ICreateProgramType, CreateProgramType>();
        services.AddScoped<IDeleteProgramType, DeleteProgramType>();
        services.AddScoped<IGetProgramTypeById, GetProgramTypeById>();
        services.AddScoped<IGetProgramTypes, GetProgramTypes>();
        services.AddScoped<IUpdateProgramType, UpdateProgramType>();
        #endregion

        #region Project
        services.AddScoped<IAppealProject, AppealProject>();
        services.AddScoped<ICancelProject, CancelProject>();
        services.AddScoped<IGetClosedProjects, GetClosedProjects>();
        services.AddScoped<IGetOpenProjects, GetOpenProjects>();
        services.AddScoped<IGetProjectById, GetProjectById>();
        services.AddScoped<IOpenProject, OpenProject>();
        services.AddScoped<ISubmitProject, SubmitProject>();
        services.AddScoped<IUpdateProject, UpdateProject>();
        #endregion

        #region ProjectEvaluation
        // services.AddScoped<IEvaluateAppealProject, EvaluateAppealProject>();
        // services.AddScoped<IEvaluateSubmissionProject, EvaluateSubmissionProject>();
        // services.AddScoped<IGetEvaluationByProjectId, GetEvaluationByProjectId>();
        #endregion

        #region Student
        services.AddScoped<ICreateStudent, CreateStudent>();
        services.AddScoped<IDeleteStudent, DeleteStudent>();
        services.AddScoped<IGetStudentById, GetStudentById>();
        services.AddScoped<IGetStudents, GetStudents>();
        services.AddScoped<IUpdateStudent, UpdateStudent>();
        #endregion

        #region TypeAssistance
        services.AddScoped<ICreateTypeAssistance, CreateTypeAssistance>();
        services.AddScoped<IDeleteTypeAssistance, DeleteTypeAssistance>();
        services.AddScoped<IGetTypeAssistanceById, GetTypeAssistanceById>();
        services.AddScoped<IGetTypeAssistances, GetTypeAssistances>();
        services.AddScoped<IUpdateTypeAssistance, UpdateTypeAssistance>();
        #endregion

        #region StudentDocuments
        // services.AddScoped<ICreateStudentDocuments, CreateStudentDocuments>();
        // services.AddScoped<IDeleteStudentDocuments, DeleteStudentDocuments>();
        // services.AddScoped<IGetStudentDocumentsByProjectId, GetStudentDocumentsByProjectId>();
        // services.AddScoped<IGetStudentDocumentsByStudentId, GetStudentDocumentsByStudentId>();
        // services.AddScoped<IUpdateStudentDocuments, UpdateStudentDocuments>();
        // services.AddScoped<ICreateStudentDocuments, CreateStudentDocuments>();
        #endregion

        #region SubArea
        services.AddScoped<ICreateSubArea, CreateSubArea>();
        services.AddScoped<IDeleteSubArea, DeleteSubArea>();
        services.AddScoped<IGetSubAreaById, GetSubAreaById>();
        services.AddScoped<IGetSubAreasByArea, GetSubAreasByArea>();
        services.AddScoped<IUpdateSubArea, UpdateSubArea>();
        #endregion

        #region User
        services.AddScoped<IActivateUser, ActivateUser>();
        services.AddScoped<IDeactivateUser, DeactivateUser>();
        services.AddScoped<IGetActiveUsers, GetActiveUsers>();
        services.AddScoped<IGetInactiveUsers, GetInactiveUsers>();
        services.AddScoped<IGetUserById, GetUserById>();
        services.AddScoped<IUpdateUser, UpdateUser>();
        #endregion

        #endregion

        #region Contract Mappers
        services.AddAutoMapper(typeof(AreaMappings));
        services.AddAutoMapper(typeof(CampusMappings));
        services.AddAutoMapper(typeof(CourseMappings));
        services.AddAutoMapper(typeof(MainAreaMappings));
        services.AddAutoMapper(typeof(NoticeMappings));
        services.AddAutoMapper(typeof(ProfessorMappings));
        services.AddAutoMapper(typeof(ProgramTypeMappings));
        services.AddAutoMapper(typeof(ProjectEvaluationMappings));
        services.AddAutoMapper(typeof(ProjectMappings));
        services.AddAutoMapper(typeof(TypeAssistanceMappings));
        services.AddAutoMapper(typeof(StudentDocumentsMappings));
        services.AddAutoMapper(typeof(StudentMappings));
        services.AddAutoMapper(typeof(SubAreaMappings));
        services.AddAutoMapper(typeof(UserMappings));
        #endregion

        return services;
    }
}