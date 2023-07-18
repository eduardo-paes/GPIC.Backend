using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class AssistanceTypeRepository : IAssistanceTypeRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public AssistanceTypeRepository(ApplicationDbContext context) => _context = context;
        #endregion

        #region Public Methods
        public async Task<AssistanceType> Create(AssistanceType model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<AssistanceType>> GetAll(int skip, int take) => await _context.AssistanceTypes
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .OrderBy(x => x.Name)
            .ToListAsync();

        public async Task<AssistanceType?> GetById(Guid? id) =>
            await _context.AssistanceTypes
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<AssistanceType> Delete(Guid? id)
        {
            var model = await GetById(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<AssistanceType> Update(AssistanceType model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<AssistanceType?> GetAssistanceTypeByName(string name)
        {
            string loweredName = name.ToLower();
            var entities = await _context.AssistanceTypes
                .Where(x => x.Name!.ToLower() == loweredName)
                .AsAsyncEnumerable()
                .ToListAsync();
            return entities.FirstOrDefault();
        }
        #endregion
    }
}