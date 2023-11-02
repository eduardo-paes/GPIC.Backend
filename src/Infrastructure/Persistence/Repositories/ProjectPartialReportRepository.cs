using System.Data.Entity;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProjectPartialReportRepository : IProjectPartialReportRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectPartialReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectPartialReport> CreateAsync(ProjectPartialReport model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ProjectPartialReport> DeleteAsync(Guid? id)
        {
            var model = await GetByIdAsync(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await UpdateAsync(model);
        }

        public async Task<IEnumerable<ProjectPartialReport>> GetAllAsync(int skip, int take)
        {
            return await _context.ProjectPartialReports
                .OrderBy(x => x.ProjectId)
                .Skip(skip)
                .Take(take)
                .AsAsyncEnumerable()
                .ToListAsync();
        }

        public async Task<ProjectPartialReport?> GetByIdAsync(Guid? id)
        {
            return await _context.ProjectPartialReports
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProjectPartialReport?> GetByProjectIdAsync(Guid? projectId)
        {
            return await _context.ProjectPartialReports
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.ProjectId == projectId);
        }

        public async Task<ProjectPartialReport> UpdateAsync(ProjectPartialReport model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}