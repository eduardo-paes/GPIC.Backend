using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public AreaRepository(ApplicationDbContext context) => _context = context;
        #endregion

        #region Public Methods
        public async Task<Area> Create(Area model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Area?> GetByCode(string? code) => await _context.Areas
            .Where(x => x.Code == code)
            .ToAsyncEnumerable()
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<Area>> GetAreasByMainArea(Guid? mainAreaId, int skip, int take) => await _context.Areas
            .Where(x => x.MainAreaId == mainAreaId)
            .Skip(skip)
            .Take(take)
            .Include(x => x.MainArea)
            .AsAsyncEnumerable()
            .OrderBy(x => x.Name)
            .ToListAsync();

        public async Task<Area?> GetById(Guid? id)
        {
            return await _context.Areas
                .Include(x => x.MainArea)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception($"Nenhuma Área encontrada para o id {id}");
        }

        public async Task<Area> Delete(Guid? id)
        {
            var model = await GetById(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<Area> Update(Area model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public Task<IEnumerable<Area>> GetAll(int skip, int take)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}