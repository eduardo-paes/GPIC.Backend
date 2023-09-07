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
        public async Task<SubArea> CreateAsync(SubArea model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<SubArea?> GetByCodeAsync(string? code)
        {
            return await _context.SubAreas
            .Where(x => x.Code == code)
            .Include(x => x.Area)
            .Include(x => x.Area != null ? x.Area.MainArea : null)
            .ToAsyncEnumerable()
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SubArea>> GetSubAreasByAreaAsync(Guid? areaId, int skip, int take)
        {
            return await _context.SubAreas
            .Where(x => x.AreaId == areaId)
            .Include(x => x.Area)
            .Include(x => x.Area != null ? x.Area.MainArea : null)
            .OrderBy(x => x.Name)
            .AsAsyncEnumerable()
            .Skip(skip)
            .Take(take)
            .ToListAsync();
        }

        public async Task<SubArea?> GetByIdAsync(Guid? id)
        {
            return await _context.SubAreas
                .Include(x => x.Area)
                .Include(x => x.Area != null ? x.Area.MainArea : null)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SubArea> DeleteAsync(Guid? id)
        {
            SubArea model = await GetByIdAsync(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await UpdateAsync(model);
        }

        public async Task<SubArea> UpdateAsync(SubArea model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public Task<IEnumerable<SubArea>> GetAllAsync(int skip, int take)
        {
            throw new NotImplementedException();
        }
        #endregion Public Methods
    }
}