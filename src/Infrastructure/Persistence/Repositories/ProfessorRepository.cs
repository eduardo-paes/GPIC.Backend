using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public ProfessorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion Global Scope

        #region Public Methods
        public async Task<Professor> CreateAsync(Professor model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<Professor>> GetAllAsync(int skip, int take)
        {
            return await _context.Professors
            .Include(x => x.User)
            .OrderBy(x => x.User!.Name)
            .AsAsyncEnumerable()
            .Skip(skip)
            .Take(take)
            .ToListAsync();
        }

        public async Task<Professor?> GetByIdAsync(Guid? id)
        {
            return await _context.Professors
                .Include(x => x.User)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Professor> DeleteAsync(Guid? id)
        {
            Professor model = await GetByIdAsync(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await UpdateAsync(model);
        }

        public async Task<Professor> UpdateAsync(Professor model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<Professor>> GetAllActiveProfessorsAsync()
        {
            return await _context.Professors
            .Include(x => x.User)
            .AsAsyncEnumerable()
            .Where(x => x.SuspensionEndDate < DateTime.UtcNow || x.SuspensionEndDate == null)
            .ToListAsync();
        }

        public async Task<Professor?> GetByUserIdAsync(Guid? userId)
        {
            return await _context.Professors
                .Include(x => x.User)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
        #endregion Public Methods
    }
}