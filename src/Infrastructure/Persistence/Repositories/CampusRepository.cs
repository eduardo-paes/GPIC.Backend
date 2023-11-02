using System.Data.Entity;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class CampusRepository : ICampusRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public CampusRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion Global Scope

        #region Public Methods
        public async Task<Campus> CreateAsync(Campus model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<Campus>> GetAllAsync(int skip, int take)
        {
            return await _context.Campuses
            .OrderBy(x => x.Name)
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .ToListAsync();
        }

        public async Task<Campus?> GetByIdAsync(Guid? id)
        {
            return await _context.Campuses
            .IgnoreQueryFilters()
            .AsAsyncEnumerable()
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Campus> DeleteAsync(Guid? id)
        {
            Campus model = await GetByIdAsync(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await UpdateAsync(model);
        }

        public async Task<Campus> UpdateAsync(Campus model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Campus?> GetCampusByNameAsync(string name)
        {
            string loweredName = name.ToLower(System.Globalization.CultureInfo.CurrentCulture);
            List<Campus> entities = await _context.Campuses
                .Where(x => x.Name!.ToLower(System.Globalization.CultureInfo.CurrentCulture) == loweredName)
                .AsAsyncEnumerable()
                .ToListAsync();
            return entities.FirstOrDefault();
        }
        #endregion Public Methods
    }
}