using Domain.Interfaces.Services;
using Domain.Mappings;
using Domain.UseCases;
using Domain.UseCases.Interactors.ActivityType;
using Domain.UseCases.Interactors.Area;
using Domain.UseCases.Interactors.AssistanceType;
using Domain.UseCases.Interactors.Auth;
using Domain.UseCases.Interactors.Campus;
using Domain.UseCases.Interactors.Course;
using Domain.UseCases.Interactors.MainArea;
using Domain.UseCases.Interactors.Notice;
using Domain.UseCases.Interactors.Professor;
using Domain.UseCases.Interactors.ProgramType;
using Domain.UseCases.Interactors.Project;
using Domain.UseCases.Interactors.ProjectEvaluation;
using Domain.UseCases.Interactors.Student;
using Domain.UseCases.Interactors.StudentDocuments;
using Domain.UseCases.Interactors.SubArea;
using Domain.UseCases.Interactors.User;
using Domain.UseCases.Interfaces.ActivityType;
using Domain.UseCases.Interfaces.Area;
using Domain.UseCases.Interfaces.AssistanceType;
using Domain.UseCases.Interfaces.Auth;
using Domain.UseCases.Interfaces.Campus;
using Domain.UseCases.Interfaces.Course;
using Domain.UseCases.Interfaces.MainArea;
using Domain.UseCases.Interfaces.Notice;
using Domain.UseCases.Interfaces.Professor;
using Domain.UseCases.Interfaces.ProgramType;
using Domain.UseCases.Interfaces.Project;
using Domain.UseCases.Interfaces.ProjectEvaluation;
using Domain.UseCases.Interfaces.Student;
using Domain.UseCases.Interfaces.StudentDocuments;
using Domain.UseCases.Interfaces.SubArea;
using Domain.UseCases.Interfaces.User;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace IoC
{
    public static class DependencyDomainInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            #region External Services
            _ = services.AddHttpContextAccessor();
            _ = services.AddScoped<IHashService, HashService>();
            _ = services.AddScoped<ITokenAuthenticationService, TokenAuthenticationService>();
            // services.AddScoped<IStorageFileService, StorageFileService>();
            _ = services.AddScoped<IStorageFileService, AzureStorageService>();
            #endregion External Services

            #region UseCases
            #region Area
            _ = services.AddScoped<ICreateArea, CreateArea>();
            _ = services.AddScoped<IDeleteArea, DeleteArea>();
            _ = services.AddScoped<IGetAreaById, GetAreaById>();
            _ = services.AddScoped<IGetAreasByMainArea, GetAreasByMainArea>();
            _ = services.AddScoped<IUpdateArea, UpdateArea>();
            #endregion Area

            #region ActivityType
            _ = services.AddScoped<IGetActivitiesByNoticeId, GetActivitiesByNoticeId>();
            _ = services.AddScoped<IGetLastNoticeActivities, GetLastNoticeActivities>();
            #endregion ActivityType

            #region AssistanceType
            _ = services.AddScoped<ICreateAssistanceType, CreateAssistanceType>();
            _ = services.AddScoped<IDeleteAssistanceType, DeleteAssistanceType>();
            _ = services.AddScoped<IGetAssistanceTypeById, GetAssistanceTypeById>();
            _ = services.AddScoped<IGetAssistanceTypes, GetAssistanceTypes>();
            _ = services.AddScoped<IUpdateAssistanceType, UpdateAssistanceType>();
            #endregion AssistanceType

            #region Auth
            _ = services.AddScoped<IConfirmEmail, ConfirmEmail>();
            _ = services.AddScoped<IForgotPassword, ForgotPassword>();
            _ = services.AddScoped<ILogin, Login>();
            _ = services.AddScoped<IResetPassword, ResetPassword>();
            #endregion Auth

            #region Campus
            _ = services.AddScoped<ICreateCampus, CreateCampus>();
            _ = services.AddScoped<IDeleteCampus, DeleteCampus>();
            _ = services.AddScoped<IGetCampusById, GetCampusById>();
            _ = services.AddScoped<IGetCampuses, GetCampuses>();
            _ = services.AddScoped<IUpdateCampus, UpdateCampus>();
            #endregion Campus

            #region Course
            _ = services.AddScoped<ICreateCourse, CreateCourse>();
            _ = services.AddScoped<IDeleteCourse, DeleteCourse>();
            _ = services.AddScoped<IGetCourseById, GetCourseById>();
            _ = services.AddScoped<IGetCourses, GetCourses>();
            _ = services.AddScoped<IUpdateCourse, UpdateCourse>();
            #endregion Course

            #region MainArea
            _ = services.AddScoped<ICreateMainArea, CreateMainArea>();
            _ = services.AddScoped<IDeleteMainArea, DeleteMainArea>();
            _ = services.AddScoped<IGetMainAreaById, GetMainAreaById>();
            _ = services.AddScoped<IGetMainAreas, GetMainAreas>();
            _ = services.AddScoped<IUpdateMainArea, UpdateMainArea>();
            #endregion MainArea

            #region Notice
            _ = services.AddScoped<ICreateNotice, CreateNotice>();
            _ = services.AddScoped<IDeleteNotice, DeleteNotice>();
            _ = services.AddScoped<IGetNoticeById, GetNoticeById>();
            _ = services.AddScoped<IGetNotices, GetNotices>();
            _ = services.AddScoped<IUpdateNotice, UpdateNotice>();
            #endregion Notice

            #region Professor
            _ = services.AddScoped<ICreateProfessor, CreateProfessor>();
            _ = services.AddScoped<IDeleteProfessor, DeleteProfessor>();
            _ = services.AddScoped<IGetProfessorById, GetProfessorById>();
            _ = services.AddScoped<IGetProfessors, GetProfessors>();
            _ = services.AddScoped<IUpdateProfessor, UpdateProfessor>();
            #endregion Professor

            #region ProgramType
            _ = services.AddScoped<ICreateProgramType, CreateProgramType>();
            _ = services.AddScoped<IDeleteProgramType, DeleteProgramType>();
            _ = services.AddScoped<IGetProgramTypeById, GetProgramTypeById>();
            _ = services.AddScoped<IGetProgramTypes, GetProgramTypes>();
            _ = services.AddScoped<IUpdateProgramType, UpdateProgramType>();
            #endregion ProgramType

            #region Project
            _ = services.AddScoped<IAppealProject, AppealProject>();
            _ = services.AddScoped<ICancelProject, CancelProject>();
            _ = services.AddScoped<IGetClosedProjects, GetClosedProjects>();
            _ = services.AddScoped<IGetOpenProjects, GetOpenProjects>();
            _ = services.AddScoped<IGetProjectById, GetProjectById>();
            _ = services.AddScoped<IOpenProject, OpenProject>();
            _ = services.AddScoped<ISubmitProject, SubmitProject>();
            _ = services.AddScoped<IUpdateProject, UpdateProject>();
            #endregion Project

            #region ProjectEvaluation
            _ = services.AddScoped<IEvaluateAppealProject, EvaluateAppealProject>();
            _ = services.AddScoped<IEvaluateSubmissionProject, EvaluateSubmissionProject>();
            _ = services.AddScoped<IGetEvaluationByProjectId, GetEvaluationByProjectId>();
            #endregion ProjectEvaluation

            #region Student
            _ = services.AddScoped<ICreateStudent, CreateStudent>();
            _ = services.AddScoped<IDeleteStudent, DeleteStudent>();
            _ = services.AddScoped<IGetStudentById, GetStudentById>();
            _ = services.AddScoped<IGetStudents, GetStudents>();
            _ = services.AddScoped<IUpdateStudent, UpdateStudent>();
            _ = services.AddScoped<IGetStudentByRegistrationCode, GetStudentByRegistrationCode>();
            _ = services.AddScoped<IRequestStudentRegister, RequestStudentRegister>();
            #endregion Student

            #region StudentDocuments
            _ = services.AddScoped<ICreateStudentDocuments, CreateStudentDocuments>();
            _ = services.AddScoped<IDeleteStudentDocuments, DeleteStudentDocuments>();
            _ = services.AddScoped<IGetStudentDocumentsByProjectId, GetStudentDocumentsByProjectId>();
            _ = services.AddScoped<IGetStudentDocumentsByStudentId, GetStudentDocumentsByStudentId>();
            _ = services.AddScoped<IUpdateStudentDocuments, UpdateStudentDocuments>();
            _ = services.AddScoped<ICreateStudentDocuments, CreateStudentDocuments>();
            #endregion StudentDocuments

            #region SubArea
            _ = services.AddScoped<ICreateSubArea, CreateSubArea>();
            _ = services.AddScoped<IDeleteSubArea, DeleteSubArea>();
            _ = services.AddScoped<IGetSubAreaById, GetSubAreaById>();
            _ = services.AddScoped<IGetSubAreasByArea, GetSubAreasByArea>();
            _ = services.AddScoped<IUpdateSubArea, UpdateSubArea>();
            #endregion SubArea

            #region User
            _ = services.AddScoped<IActivateUser, ActivateUser>();
            _ = services.AddScoped<IDeactivateUser, DeactivateUser>();
            _ = services.AddScoped<IGetActiveUsers, GetActiveUsers>();
            _ = services.AddScoped<IGetInactiveUsers, GetInactiveUsers>();
            _ = services.AddScoped<IGetUserById, GetUserById>();
            _ = services.AddScoped<IUpdateUser, UpdateUser>();
            #endregion User

            #endregion UseCases

            #region Contract Mappers
            _ = services.AddAutoMapper(typeof(AreaMappings));
            _ = services.AddAutoMapper(typeof(ActivityMappings));
            _ = services.AddAutoMapper(typeof(AssistanceTypeMappings));
            _ = services.AddAutoMapper(typeof(CampusMappings));
            _ = services.AddAutoMapper(typeof(CourseMappings));
            _ = services.AddAutoMapper(typeof(MainAreaMappings));
            _ = services.AddAutoMapper(typeof(NoticeMappings));
            _ = services.AddAutoMapper(typeof(ProfessorMappings));
            _ = services.AddAutoMapper(typeof(ProgramTypeMappings));
            _ = services.AddAutoMapper(typeof(ProjectEvaluationMappings));
            _ = services.AddAutoMapper(typeof(ProjectMappings));
            _ = services.AddAutoMapper(typeof(StudentDocumentsMappings));
            _ = services.AddAutoMapper(typeof(StudentMappings));
            _ = services.AddAutoMapper(typeof(SubAreaMappings));
            _ = services.AddAutoMapper(typeof(UserMappings));
            #endregion Contract Mappers

            return services;
        }
    }
}