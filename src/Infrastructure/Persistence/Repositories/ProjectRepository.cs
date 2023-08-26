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
            _ = _context.Add(project);
            _ = await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> UpdateAsync(Project project)
        {
            _ = _context.Update(project);
            _ = await _context.SaveChangesAsync();
            return project;
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
            return isClosed
                ? await _context.Projects
                    .Include(x => x.Student)
                    .Include(x => x.Professor)
                    .Include(x => x.SubArea)
                    .Include(x => x.ProgramType)
                    .Include(x => x.Notice)
                    .IgnoreQueryFilters()
                    .AsAsyncEnumerable()
                    .Where(x => x.StudentId == id
                        && (x.Status == EProjectStatus.Closed
                            || x.Status == EProjectStatus.Canceled))
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
                .Where(x => x.StudentId == id
                    && x.Status != EProjectStatus.Closed
                    && x.Status != EProjectStatus.Canceled)
                .OrderByDescending(x => x.Notice?.RegistrationStartDate) // Traz os projetos mais recentes primeiro.
                .ThenBy(x => x.Title) // Traz os projetos em ordem alfabética.
                .Skip(skip)
                .Take(take)
                .ToListAsync();
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
            return isClosed
                ? await _context.Projects
                    .Include(x => x.Student)
                    .Include(x => x.Professor)
                    .Include(x => x.SubArea)
                    .Include(x => x.ProgramType)
                    .Include(x => x.Notice)
                    .IgnoreQueryFilters()
                    .AsAsyncEnumerable()
                    .Where(x => x.ProfessorId == id
                        && (x.Status == EProjectStatus.Closed
                            || x.Status == EProjectStatus.Canceled))
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
                .Where(x => x.StudentId == id
                    && x.Status != EProjectStatus.Closed
                    && x.Status != EProjectStatus.Canceled)
                .OrderByDescending(x => x.Notice?.RegistrationStartDate) // Traz os projetos mais recentes primeiro.
                .ThenBy(x => x.Title) // Traz os projetos em ordem alfabética.
                .Skip(skip)
                .Take(take)
                .ToListAsync();
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
    }
}