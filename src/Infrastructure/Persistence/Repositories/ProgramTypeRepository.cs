using System.Data.Entity;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProgramTypeRepository : IProgramTypeRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public ProgramTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion Global Scope

        #region Public Methods
        public async Task<ProgramType> CreateAsync(ProgramType model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<ProgramType>> GetAllAsync(int skip, int take)
        {
            return await _context.ProgramTypes
            .OrderBy(x => x.Name)
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .ToListAsync();
        }

        public async Task<ProgramType?> GetByIdAsync(Guid? id)
        {
            return await _context.ProgramTypes
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProgramType> DeleteAsync(Guid? id)
        {
            ProgramType model = await GetByIdAsync(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await UpdateAsync(model);
        }

        public async Task<ProgramType> UpdateAsync(ProgramType model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ProgramType?> GetProgramTypeByNameAsync(string name)
        {
            string loweredName = name.ToLower(System.Globalization.CultureInfo.CurrentCulture);
            List<ProgramType> entities = await _context.ProgramTypes
                .Where(x => x.Name!.ToLower(System.Globalization.CultureInfo.CurrentCulture) == loweredName)
                .AsAsyncEnumerable()
                .ToListAsync();
            return entities.FirstOrDefault();
        }
        #endregion Public Methods
    }
}