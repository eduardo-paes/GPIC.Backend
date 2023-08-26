using Domain.Interfaces.Repositories;
using Infrastructure.IoC.Utils;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Infrastructure.IoC
{
    public static class PersistenceDI
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            // Carrega informações de ambiente (.env)
            DotEnvSecrets dotEnvSecrets = new();

            #region Inicialização do banco de dados
            services.AddDbContext<ApplicationDbContext>(
                o => o.UseNpgsql(dotEnvSecrets.GetDatabaseConnectionString(),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            #endregion Inicialização do banco de dados

            #region Repositórios
            services.AddScoped<IAreaRepository, AreaRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IActivityTypeRepository, ActivityTypeRepository>();
            services.AddScoped<IAssistanceTypeRepository, AssistanceTypeRepository>();
            services.AddScoped<ICampusRepository, CampusRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IMainAreaRepository, MainAreaRepository>();
            services.AddScoped<INoticeRepository, NoticeRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<IProgramTypeRepository, ProgramTypeRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectFinalReportRepository, ProjectFinalReportRepository>();
            services.AddScoped<IProjectPartialReportRepository, ProjectPartialReportRepository>();
            services.AddScoped<IProjectActivityRepository, ProjectActivityRepository>();
            services.AddScoped<IProjectEvaluationRepository, ProjectEvaluationRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentDocumentsRepository, StudentDocumentsRepository>();
            services.AddScoped<ISubAreaRepository, SubAreaRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion Repositórios

            return services;
        }
    }
}