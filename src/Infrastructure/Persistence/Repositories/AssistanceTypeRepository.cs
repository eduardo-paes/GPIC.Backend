using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class AssistanceTypeRepository : IAssistanceTypeRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public AssistanceTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion Global Scope

        #region Public Methods
        public async Task<AssistanceType> CreateAsync(AssistanceType model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<AssistanceType>> GetAllAsync(int skip, int take)
        {
            return await _context.AssistanceTypes
            .OrderBy(x => x.Name)
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .ToListAsync();
        }

        public async Task<AssistanceType?> GetByIdAsync(Guid? id)
        {
            return await _context.AssistanceTypes
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AssistanceType> DeleteAsync(Guid? id)
        {
            AssistanceType model = await GetByIdAsync(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await UpdateAsync(model);
        }

        public async Task<AssistanceType> UpdateAsync(AssistanceType model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<AssistanceType?> GetAssistanceTypeByNameAsync(string name)
        {
            string loweredName = name.ToLower(System.Globalization.CultureInfo.CurrentCulture);
            List<AssistanceType> entities = await _context.AssistanceTypes
                .Where(x => x.Name!.ToLower(System.Globalization.CultureInfo.CurrentCulture) == loweredName)
                .AsAsyncEnumerable()
                .ToListAsync();
            return entities.FirstOrDefault();
        }
        #endregion Public Methods
    }
}