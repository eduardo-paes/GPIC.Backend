using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ProjectEvaluationRepository : IProjectEvaluationRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectEvaluationRepository(ApplicationDbContext context) => _context = context;

        public async Task<ProjectEvaluation> Create(ProjectEvaluation model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ProjectEvaluation?> GetById(Guid? id)
        {
            return await _context.ProjectEvaluations
                .Include(x => x.ProjectId)
                .Include(x => x.SubmissionEvaluatorId)
                .Include(x => x.AppealEvaluatorId)
                .Include(x => x.DocumentsEvaluatorId)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception($"Nenhuma avaliação encontrada para o id {id}");
        }

        public async Task<ProjectEvaluation?> GetByProjectId(Guid? projectId)
        {
            return await _context.ProjectEvaluations
                .Include(x => x.ProjectId)
                .Include(x => x.SubmissionEvaluatorId)
                .Include(x => x.AppealEvaluatorId)
                .Include(x => x.DocumentsEvaluatorId)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.ProjectId == projectId)
                ?? throw new Exception($"Nenhuma avaliação encontrada para o ProjectId {projectId}");
        }

        public async Task<ProjectEvaluation> Update(ProjectEvaluation model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}