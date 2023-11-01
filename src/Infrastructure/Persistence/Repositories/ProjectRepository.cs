using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Project> CreateAsync(Project project)
        {
            _context.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> UpdateAsync(Project project)
        {
            _context.Update(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<int> UpdateManyAsync(IList<Project> projects)
        {
            _context.Update(projects);
            return await _context.SaveChangesAsync();
        }

        public async Task<Project> DeleteAsync(Guid? id)
        {
            Project project = await GetByIdAsync(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            project.DeactivateEntity();
            return await UpdateAsync(project);
        }

        public async Task<Project?> GetByIdAsync(Guid? id)
        {
            return await _context.Projects
                .Include(x => x.Notice)
                .Include(x => x.SubArea)
                .Include(x => x.Student)
                .Include(x => x.Professor)
                .Include(x => x.ProgramType)
                .Include(x => x.Professor!.User)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception($"Nenhum Projeto encontrado para o id {id}");
        }

        public async Task<IEnumerable<Project>> GetProfessorProjectsAsync(int skip, int take, Guid? id, bool isClosed = false)
        {
            return await _context.Projects
                .Include(x => x.Student)
                .Include(x => x.Student!.User)
                .Include(x => x.Professor)
                .Include(x => x.Professor!.User)
                .Include(x => x.SubArea)
                .Include(x => x.ProgramType)
                .Include(x => x.Notice)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .Where(x => WhereClause(id, x))
                .OrderByDescending(x => (x.Notice?.RegistrationStartDate)) // Traz os projetos mais recentes primeiro.
                .ThenBy(x => x.Title) // Traz os projetos em ordem alfabética.
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            bool WhereClause(Guid? id, Project p)
            {
                return isClosed
                    ? p.ProfessorId == id && (p.Status == EProjectStatus.Closed || p.Status == EProjectStatus.Canceled)
                    : p.ProfessorId == id && p.Status != EProjectStatus.Closed && p.Status != EProjectStatus.Canceled;
            }
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync(int skip, int take, bool isClosed = false)
        {
            return isClosed
                ? await _context.Projects
                    .Include(x => x.Student)
                    .Include(x => x.Professor)
                    .Include(x => x.SubArea)
                    .Include(x => x.ProgramType)
                    .Include(x => x.Notice)
                    .IgnoreQueryFilters()
                    .AsAsyncEnumerable()
                    .Where(x => x.Status is EProjectStatus.Closed or EProjectStatus.Canceled)
                    .OrderByDescending(x => x.Notice?.RegistrationStartDate) // Traz os projetos mais recentes primeiro.
                    .ThenBy(x => x.Title) // Traz os projetos em ordem alfabética.
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync()
                : (IEnumerable<Project>)await _context.Projects
                .Include(x => x.Student)
                .Include(x => x.Professor)
                .Include(x => x.SubArea)
                .Include(x => x.ProgramType)
                .Include(x => x.Notice)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .Where(x => x.Status is not EProjectStatus.Closed and not EProjectStatus.Canceled)
                .OrderByDescending(x => x.Notice?.RegistrationStartDate) // Traz os projetos mais recentes primeiro.
                .ThenBy(x => x.Title) // Traz os projetos em ordem alfabética.
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetStudentProjectsAsync(int skip, int take, Guid? id, bool isClosed = false)
        {
            return await _context.Projects
                .Include(x => x.Student)
                .Include(x => x.Student!.User)
                .Include(x => x.Professor)
                .Include(x => x.Professor!.User)
                .Include(x => x.SubArea)
                .Include(x => x.ProgramType)
                .Include(x => x.Notice)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .Where(x => WhereClause(id, x))
                .OrderByDescending(x => x.Notice?.RegistrationStartDate) // Traz os projetos mais recentes primeiro.
                .ThenBy(x => x.Title) // Traz os projetos em ordem alfabética.
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            bool WhereClause(Guid? id, Project p)
            {
                return isClosed
                    ? p.StudentId == id && (p.Status == EProjectStatus.Closed || p.Status == EProjectStatus.Canceled)
                    : p.StudentId == id && p.Status != EProjectStatus.Closed && p.Status != EProjectStatus.Canceled;
            }
        }

        public async Task<IEnumerable<Project>> GetProjectByNoticeAsync(Guid? noticeId)
        {
            return await _context.Projects
                .Include(x => x.Student)
                .Include(x => x.Student!.User)
                .Include(x => x.Professor)
                .Include(x => x.Professor!.User)
                .Include(x => x.SubArea)
                .Include(x => x.ProgramType)
                .Include(x => x.Notice)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .Where(x => x.NoticeId == noticeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectsToEvaluateAsync(int skip, int take, Guid? professorId)
        {
            return await _context.Projects
                .Include(x => x.Student)
                .Include(x => x.Professor)
                .Include(x => x.SubArea)
                .Include(x => x.ProgramType)
                .Include(x => x.Notice)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .Where(x => x.Status is EProjectStatus.Submitted or EProjectStatus.Evaluation or EProjectStatus.DocumentAnalysis
                    && x.ProfessorId != professorId)
                .OrderByDescending(x => x.Notice?.RegistrationStartDate) // Traz os projetos mais recentes primeiro.
                .ThenBy(x => x.Title) // Traz os projetos em ordem alfabética.
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectsWithCloseReportDueDateAsync()
        {
            DateTime nextMonth = DateTime.UtcNow.AddMonths(1);
            DateTime nextWeek = DateTime.UtcNow.AddDays(7);
            return await _context.Projects
                .Include(x => x.Professor)
                .Include(x => x.Notice)
                .AsAsyncEnumerable()
                .Where(x =>
                    // Contabiliza apenas projetos que estejam no status Iniciado
                    x.Status is EProjectStatus.Started
                    && (
                        // Data de entrega do relatório parcial deverá ocorrer dentro de 1 mês
                        (x.Notice!.PartialReportDeadline.HasValue && x.Notice.PartialReportDeadline.Value.Date == nextMonth.Date) ||
                        // Data de entrega do relatório final deverá ocorrer dentro de 1 mês
                        (x.Notice!.FinalReportDeadline.HasValue && x.Notice.FinalReportDeadline.Value.Date == nextMonth.Date) ||
                        // Data de entrega do relatório parcial deverá ocorrer dentro de 7 dias
                        (x.Notice!.PartialReportDeadline.HasValue && x.Notice.PartialReportDeadline.Value.Date == nextWeek.Date) ||
                        // Data de entrega do relatório final deverá ocorrer dentro de 7 dias
                        (x.Notice!.FinalReportDeadline.HasValue && x.Notice.FinalReportDeadline.Value.Date == nextWeek.Date)
                    ))
                .ToListAsync();
        }

        public async Task<IList<Project>> GetPendingAndOverdueProjectsAsync()
        {
            // Obtém data atual em UTC para comparação
            DateTime currentDate = DateTime.UtcNow;

            // Obtém projetos pendentes e com o prazo de resolução vencido
            return await _context.Projects
                .Include(x => x.Notice)
                .AsAsyncEnumerable()
                .Where(x =>
                    // Projetos abertos e com o prazo de submissão vencido
                    (x.Status is EProjectStatus.Opened && x.Notice!.RegistrationEndDate < currentDate) ||
                    // Projetos rejeitados e com o prazo de recurso vencido
                    (x.Status is EProjectStatus.Rejected && x.Notice!.AppealEndDate < currentDate) ||
                    // Projetos aprovados e com entrega de documentação do aluno vencida
                    (x.Status is EProjectStatus.Accepted && x.Notice!.SendingDocsEndDate < currentDate) ||
                    // Projetos pendentes de documentação e com o prazo de entrega vencido
                    (x.Status is EProjectStatus.Pending && x.Notice!.SendingDocsEndDate < currentDate))
                .ToListAsync();
        }
    }
}