using System.Data.Entity;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class MainAreaRepository : IMainAreaRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public MainAreaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion Global Scope

        #region Public Methods
        public async Task<MainArea> CreateAsync(MainArea model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<MainArea?> GetByCodeAsync(string? code)
        {
            return await _context.MainAreas
            .Where(x => x.Code == code)
            .ToAsyncEnumerable()
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MainArea>> GetAllAsync(int skip, int take)
        {
            return await _context.MainAreas
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .OrderBy(x => x.Name)
            .ToListAsync();
        }

        public async Task<MainArea?> GetByIdAsync(Guid? id)
        {
            return await _context.MainAreas
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MainArea> DeleteAsync(Guid? id)
        {
            MainArea model = await GetByIdAsync(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await UpdateAsync(model);
        }

        public async Task<MainArea> UpdateAsync(MainArea model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }
        #endregion Public Methods
    }
}