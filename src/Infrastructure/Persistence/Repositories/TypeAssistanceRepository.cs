using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class TypeAssistanceRepository : ITypeAssistanceRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public TypeAssistanceRepository(ApplicationDbContext context) => _context = context;
        #endregion

        #region Public Methods
        public async Task<TypeAssistance> Create(TypeAssistance model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<TypeAssistance>> GetAll(int skip, int take) => await _context.TypeAssistances
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .OrderBy(x => x.Name)
            .ToListAsync();

        public async Task<TypeAssistance?> GetById(Guid? id) =>
            await _context.TypeAssistances
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<TypeAssistance> Delete(Guid? id)
        {
            var model = await GetById(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<TypeAssistance> Update(TypeAssistance model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<TypeAssistance?> GetTypeAssistanceByName(string name)
        {
            string loweredName = name.ToLower();
            var entities = await _context.TypeAssistances
                .Where(x => x.Name!.ToLower() == loweredName)
                .AsAsyncEnumerable()
                .ToListAsync();
            return entities.FirstOrDefault();
        }
        #endregion
    }
}