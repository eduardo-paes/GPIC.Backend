using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProjectEvaluationRepository : IProjectEvaluationRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectEvaluationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectEvaluation> CreateAsync(ProjectEvaluation model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ProjectEvaluation?> GetByIdAsync(Guid? id)
        {
            return await _context.ProjectEvaluations
                .Include(x => x.Project)
                .Include(x => x.SubmissionEvaluator)
                .Include(x => x.AppealEvaluator)
                .Include(x => x.DocumentsEvaluator)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception($"Nenhuma avaliação encontrada para o id {id}");
        }

        public async Task<ProjectEvaluation?> GetByProjectIdAsync(Guid? projectId)
        {
            return await _context.ProjectEvaluations
                .Include(x => x.Project)
                .Include(x => x.SubmissionEvaluator)
                .Include(x => x.AppealEvaluator)
                .Include(x => x.DocumentsEvaluator)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.ProjectId == projectId)
                ?? throw new Exception($"Nenhuma avaliação encontrada para o ProjectId {projectId}");
        }

        public async Task<ProjectEvaluation> UpdateAsync(ProjectEvaluation model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }
    }
}