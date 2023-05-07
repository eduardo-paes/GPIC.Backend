using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public ProfessorRepository(ApplicationDbContext context) => _context = context;
        #endregion

        #region Public Methods
        public async Task<Professor> Create(Professor model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<Professor>> GetAll(int skip, int take) => await _context.Professors
            .Include(x => x.User)
            .Where(x => x.DeletedAt == null)
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .OrderBy(x => x.User?.Name)
            .ToListAsync();

        public async Task<Professor?> GetById(Guid? id) =>
            await _context.Professors
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Professor> Delete(Guid? id)
        {
            var model = await GetById(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<Professor> Update(Professor model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
        #endregion
    }
}