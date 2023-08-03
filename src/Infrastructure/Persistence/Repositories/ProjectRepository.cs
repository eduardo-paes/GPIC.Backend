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

        public async Task<Project> CreateAsync(Project model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Project> UpdateAsync(Project model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Project> DeleteAsync(Guid? id)
        {
            Project model = await GetByIdAsync(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await UpdateAsync(model);
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
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }
    }
}