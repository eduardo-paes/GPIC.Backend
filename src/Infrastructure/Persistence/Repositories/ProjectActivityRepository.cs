using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProjectActivityRepository : IProjectActivityRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectActivity> Create(ProjectActivity model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<ProjectActivity>> GetAll(int skip, int take)
        {
            return await _context.ProjectActivities
                .Skip(skip)
                .Take(take)
                .AsAsyncEnumerable()
                .ToListAsync();
        }

        public async Task<ProjectActivity?> GetById(Guid? id)
        {
            return await _context.ProjectActivities
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProjectActivity> Delete(Guid? id)
        {
            ProjectActivity model = await GetById(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<ProjectActivity> Update(ProjectActivity model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IList<ProjectActivity>> GetByProjectId(Guid? projectId)
        {
            return await _context.ProjectActivities
                .AsAsyncEnumerable()
                .Where(x => x.ProjectId == projectId)
                .ToListAsync();
        }
    }
}