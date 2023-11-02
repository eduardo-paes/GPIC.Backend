using System.Data.Entity;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProjectFinalReportRepository : IProjectFinalReportRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectFinalReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectFinalReport> CreateAsync(ProjectFinalReport model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ProjectFinalReport> DeleteAsync(Guid? id)
        {
            var model = await GetByIdAsync(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await UpdateAsync(model);
        }

        public async Task<IEnumerable<ProjectFinalReport>> GetAllAsync(int skip, int take)
        {
            return await _context.ProjectFinalReports
                .OrderByDescending(x => x.SendDate)
                .Skip(skip)
                .Take(take)
                .AsAsyncEnumerable()
                .ToListAsync();
        }

        public async Task<ProjectFinalReport?> GetByIdAsync(Guid? id)
        {
            return await _context.ProjectFinalReports
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProjectFinalReport?> GetByProjectIdAsync(Guid? projectId)
        {
            return await _context.ProjectFinalReports
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.ProjectId == projectId);
        }

        public async Task<ProjectFinalReport> UpdateAsync(ProjectFinalReport model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}