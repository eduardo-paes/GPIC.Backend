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
        public ProjectRepository(ApplicationDbContext context) => _context = context;

        public async Task<Project> Create(Project model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Project> Update(Project model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Project> Delete(Guid? id)
        {
            var model = await GetById(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<Project?> GetById(Guid? id)
        {
            return await _context.Projects
                .Include(x => x.Student)
                .Include(x => x.Professor)
                .Include(x => x.SubArea)
                .Include(x => x.ProgramType)
                .Include(x => x.Notice)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception($"Nenhum Projeto encontrado para o id {id}");
        }

        public async Task<IEnumerable<Project>> GetProfessorProjects(int skip, int take, Guid? id, bool isClosed = false)
        {
            if (isClosed)
            {
                return await _context.Projects
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
                    .ToListAsync();
            }

            return await _context.Projects
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

        public async Task<IEnumerable<Project>> GetProjects(int skip, int take, bool isClosed = false)
        {
            if (isClosed)
            {
                return await _context.Projects
                    .Include(x => x.Student)
                    .Include(x => x.Professor)
                    .Include(x => x.SubArea)
                    .Include(x => x.ProgramType)
                    .Include(x => x.Notice)
                    .IgnoreQueryFilters()
                    .AsAsyncEnumerable()
                    .Where(x => x.Status == EProjectStatus.Closed || x.Status == EProjectStatus.Canceled)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
            }

            return await _context.Projects
                .Include(x => x.Student)
                .Include(x => x.Professor)
                .Include(x => x.SubArea)
                .Include(x => x.ProgramType)
                .Include(x => x.Notice)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .Where(x => x.Status != EProjectStatus.Closed && x.Status != EProjectStatus.Canceled)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetStudentProjects(int skip, int take, Guid? id, bool isClosed = false)
        {
            if (isClosed)
            {
                return await _context.Projects
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
                    .ToListAsync();
            }

            return await _context.Projects
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