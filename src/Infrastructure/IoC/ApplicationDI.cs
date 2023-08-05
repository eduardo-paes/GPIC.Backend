using Domain.Mappings;
using Application.UseCases.ActivityType;
using Application.UseCases.Area;
using Application.UseCases.AssistanceType;
using Application.UseCases.Auth;
using Application.UseCases.Campus;
using Application.UseCases.Course;
using Application.UseCases.MainArea;
using Application.UseCases.Notice;
using Application.UseCases.Professor;
using Application.UseCases.ProgramType;
using Application.UseCases.Project;
using Application.UseCases.ProjectEvaluation;
using Application.UseCases.Student;
using Application.UseCases.StudentDocuments;
using Application.UseCases.SubArea;
using Application.UseCases.User;
using Application.Interfaces.UseCases.ActivityType;
using Application.Interfaces.UseCases.Area;
using Application.Interfaces.UseCases.AssistanceType;
using Application.Interfaces.UseCases.Auth;
using Application.Interfaces.UseCases.Campus;
using Application.Interfaces.UseCases.Course;
using Application.Interfaces.UseCases.MainArea;
using Application.Interfaces.UseCases.Notice;
using Application.Interfaces.UseCases.Professor;
using Application.Interfaces.UseCases.ProgramType;
using Application.Interfaces.UseCases.Project;
using Application.Interfaces.UseCases.ProjectEvaluation;
using Application.Interfaces.UseCases.Student;
using Application.Interfaces.UseCases.StudentDocuments;
using Application.Interfaces.UseCases.SubArea;
using Application.Interfaces.UseCases.User;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            #region UseCases
            #region Area
            services.AddScoped<ICreateArea, CreateArea>();
            services.AddScoped<IDeleteArea, DeleteArea>();
            services.AddScoped<IGetAreaById, GetAreaById>();
            services.AddScoped<IGetAreasByMainArea, GetAreasByMainArea>();
            services.AddScoped<IUpdateArea, UpdateArea>();
            #endregion Area

            #region ActivityType
            services.AddScoped<IGetActivitiesByNoticeId, GetActivitiesByNoticeId>();
            services.AddScoped<IGetLastNoticeActivities, GetLastNoticeActivities>();
            #endregion ActivityType

            #region AssistanceType
            services.AddScoped<ICreateAssistanceType, CreateAssistanceType>();
            services.AddScoped<IDeleteAssistanceType, DeleteAssistanceType>();
            services.AddScoped<IGetAssistanceTypeById, GetAssistanceTypeById>();
            services.AddScoped<IGetAssistanceTypes, GetAssistanceTypes>();
            services.AddScoped<IUpdateAssistanceType, UpdateAssistanceType>();
            #endregion AssistanceType

            #region Auth
            services.AddScoped<IConfirmEmail, ConfirmEmail>();
            services.AddScoped<IForgotPassword, ForgotPassword>();
            services.AddScoped<ILogin, Login>();
            services.AddScoped<IResetPassword, ResetPassword>();
            #endregion Auth

            #region Campus
            services.AddScoped<ICreateCampus, CreateCampus>();
            services.AddScoped<IDeleteCampus, DeleteCampus>();
            services.AddScoped<IGetCampusById, GetCampusById>();
            services.AddScoped<IGetCampuses, GetCampuses>();
            services.AddScoped<IUpdateCampus, UpdateCampus>();
            #endregion Campus

            #region Course
            services.AddScoped<ICreateCourse, CreateCourse>();
            services.AddScoped<IDeleteCourse, DeleteCourse>();
            services.AddScoped<IGetCourseById, GetCourseById>();
            services.AddScoped<IGetCourses, GetCourses>();
            services.AddScoped<IUpdateCourse, UpdateCourse>();
            #endregion Course

            #region MainArea
            services.AddScoped<ICreateMainArea, CreateMainArea>();
            services.AddScoped<IDeleteMainArea, DeleteMainArea>();
            services.AddScoped<IGetMainAreaById, GetMainAreaById>();
            services.AddScoped<IGetMainAreas, GetMainAreas>();
            services.AddScoped<IUpdateMainArea, UpdateMainArea>();
            #endregion MainArea

            #region Notice
            services.AddScoped<ICreateNotice, CreateNotice>();
            services.AddScoped<IDeleteNotice, DeleteNotice>();
            services.AddScoped<IGetNoticeById, GetNoticeById>();
            services.AddScoped<IGetNotices, GetNotices>();
            services.AddScoped<IUpdateNotice, UpdateNotice>();
            #endregion Notice

            #region Professor
            services.AddScoped<ICreateProfessor, CreateProfessor>();
            services.AddScoped<IDeleteProfessor, DeleteProfessor>();
            services.AddScoped<IGetProfessorById, GetProfessorById>();
            services.AddScoped<IGetProfessors, GetProfessors>();
            services.AddScoped<IUpdateProfessor, UpdateProfessor>();
            #endregion Professor

            #region ProgramType
            services.AddScoped<ICreateProgramType, CreateProgramType>();
            services.AddScoped<IDeleteProgramType, DeleteProgramType>();
            services.AddScoped<IGetProgramTypeById, GetProgramTypeById>();
            services.AddScoped<IGetProgramTypes, GetProgramTypes>();
            services.AddScoped<IUpdateProgramType, UpdateProgramType>();
            #endregion ProgramType

            #region Project
            services.AddScoped<IAppealProject, AppealProject>();
            services.AddScoped<ICancelProject, CancelProject>();
            services.AddScoped<IGetClosedProjects, GetClosedProjects>();
            services.AddScoped<IGetOpenProjects, GetOpenProjects>();
            services.AddScoped<IGetProjectById, GetProjectById>();
            services.AddScoped<IOpenProject, OpenProject>();
            services.AddScoped<ISubmitProject, SubmitProject>();
            services.AddScoped<IUpdateProject, UpdateProject>();
            #endregion Project

            #region ProjectEvaluation
            services.AddScoped<IEvaluateAppealProject, EvaluateAppealProject>();
            services.AddScoped<IEvaluateSubmissionProject, EvaluateSubmissionProject>();
            services.AddScoped<IGetEvaluationByProjectId, GetEvaluationByProjectId>();
            #endregion ProjectEvaluation

            #region Student
            services.AddScoped<ICreateStudent, CreateStudent>();
            services.AddScoped<IDeleteStudent, DeleteStudent>();
            services.AddScoped<IGetStudentById, GetStudentById>();
            services.AddScoped<IGetStudents, GetStudents>();
            services.AddScoped<IUpdateStudent, UpdateStudent>();
            services.AddScoped<IGetStudentByRegistrationCode, GetStudentByRegistrationCode>();
            services.AddScoped<IRequestStudentRegister, RequestStudentRegister>();
            #endregion Student

            #region StudentDocuments
            services.AddScoped<ICreateStudentDocuments, CreateStudentDocuments>();
            services.AddScoped<IDeleteStudentDocuments, DeleteStudentDocuments>();
            services.AddScoped<IGetStudentDocumentsByProjectId, GetStudentDocumentsByProjectId>();
            services.AddScoped<IGetStudentDocumentsByStudentId, GetStudentDocumentsByStudentId>();
            services.AddScoped<IUpdateStudentDocuments, UpdateStudentDocuments>();
            services.AddScoped<ICreateStudentDocuments, CreateStudentDocuments>();
            #endregion StudentDocuments

            #region SubArea
            services.AddScoped<ICreateSubArea, CreateSubArea>();
            services.AddScoped<IDeleteSubArea, DeleteSubArea>();
            services.AddScoped<IGetSubAreaById, GetSubAreaById>();
            services.AddScoped<IGetSubAreasByArea, GetSubAreasByArea>();
            services.AddScoped<IUpdateSubArea, UpdateSubArea>();
            #endregion SubArea

            #region User
            services.AddScoped<IActivateUser, ActivateUser>();
            services.AddScoped<IDeactivateUser, DeactivateUser>();
            services.AddScoped<IGetActiveUsers, GetActiveUsers>();
            services.AddScoped<IGetInactiveUsers, GetInactiveUsers>();
            services.AddScoped<IGetUserById, GetUserById>();
            services.AddScoped<IUpdateUser, UpdateUser>();
            #endregion User

            #endregion UseCases

            #region Port Mappers
            services.AddAutoMapper(typeof(AreaMappings));
            services.AddAutoMapper(typeof(ActivityMappings));
            services.AddAutoMapper(typeof(AssistanceTypeMappings));
            services.AddAutoMapper(typeof(CampusMappings));
            services.AddAutoMapper(typeof(CourseMappings));
            services.AddAutoMapper(typeof(MainAreaMappings));
            services.AddAutoMapper(typeof(NoticeMappings));
            services.AddAutoMapper(typeof(ProfessorMappings));
            services.AddAutoMapper(typeof(ProgramTypeMappings));
            services.AddAutoMapper(typeof(ProjectEvaluationMappings));
            services.AddAutoMapper(typeof(ProjectMappings));
            services.AddAutoMapper(typeof(StudentDocumentsMappings));
            services.AddAutoMapper(typeof(StudentMappings));
            services.AddAutoMapper(typeof(SubAreaMappings));
            services.AddAutoMapper(typeof(UserMappings));
            #endregion Port Mappers

            return services;
        }
    }
}