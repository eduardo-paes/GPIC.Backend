using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public AreaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion Global Scope

        #region Public Methods
        public async Task<Area> CreateAsync(Area model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Area?> GetByCodeAsync(string? code)
        {
            return await _context.Areas
            .Where(x => x.Code == code)
            .ToAsyncEnumerable()
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Area>> GetAreasByMainAreaAsync(Guid? mainAreaId, int skip, int take)
        {
            return await _context.Areas
            .Where(x => x.MainAreaId == mainAreaId)
            .Skip(skip)
            .Take(take)
            .Include(x => x.MainArea)
            .AsAsyncEnumerable()
            .OrderBy(x => x.Name)
            .ToListAsync();
        }

        public async Task<Area?> GetByIdAsync(Guid? id)
        {
            return await _context.Areas
                .Include(x => x.MainArea)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception($"Nenhuma Área encontrada para o id {id}");
        }

        public async Task<Area> DeleteAsync(Guid? id)
        {
            Area model = await GetByIdAsync(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await UpdateAsync(model);
        }

        public async Task<Area> UpdateAsync(Area model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public Task<IEnumerable<Area>> GetAllAsync(int skip, int take)
        {
            throw new NotImplementedException();
        }
        #endregion Public Methods
    }
}