using System.Data.Entity;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProjectReportRepository : IProjectReportRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectReport> CreateAsync(ProjectReport model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ProjectReport> DeleteAsync(Guid? id)
        {
            var model = await GetByIdAsync(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await UpdateAsync(model);
        }

        public async Task<IEnumerable<ProjectReport>> GetAllAsync(int skip, int take)
        {
            return await _context.ProjectReports
                .Skip(skip)
                .Take(take)
                .AsAsyncEnumerable()
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<ProjectReport?> GetByIdAsync(Guid? id)
        {
            return await _context.ProjectReports
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<ProjectReport>?> GetByProjectIdAsync(Guid? projectId)
        {
            return await _context.ProjectReports
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .Where(x => x.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<ProjectReport> UpdateAsync(ProjectReport model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}