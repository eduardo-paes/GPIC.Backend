using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class SubAreaRepository : ISubAreaRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public SubAreaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion Global Scope

        #region Public Methods
        public async Task<SubArea> Create(SubArea model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<SubArea?> GetByCode(string? code)
        {
            return await _context.SubAreas
            .Where(x => x.Code == code)
            .Include(x => x.Area)
            .Include(x => x.Area != null ? x.Area.MainArea : null)
            .ToAsyncEnumerable()
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SubArea>> GetSubAreasByArea(Guid? areaId, int skip, int take)
        {
            return await _context.SubAreas
            .Where(x => x.AreaId == areaId)
            .Skip(skip)
            .Take(take)
            .Include(x => x.Area)
            .Include(x => x.Area != null ? x.Area.MainArea : null)
            .AsAsyncEnumerable()
            .OrderBy(x => x.Name)
            .ToListAsync();
        }

        public async Task<SubArea?> GetById(Guid? id)
        {
            return await _context.SubAreas
                .Include(x => x.Area)
                .Include(x => x.Area != null ? x.Area.MainArea : null)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SubArea> Delete(Guid? id)
        {
            SubArea model = await GetById(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<SubArea> Update(SubArea model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public Task<IEnumerable<SubArea>> GetAll(int skip, int take)
        {
            throw new NotImplementedException();
        }
        #endregion Public Methods
    }
}