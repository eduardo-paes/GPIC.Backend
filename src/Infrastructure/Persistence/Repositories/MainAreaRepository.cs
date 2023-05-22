using System.Data.Entity;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class MainAreaRepository : IMainAreaRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public MainAreaRepository(ApplicationDbContext context) => _context = context;
        #endregion

        #region Public Methods
        public async Task<MainArea> Create(MainArea model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<MainArea?> GetByCode(string? code) => await _context.MainAreas
            .Where(x => x.Code == code)
            .ToAsyncEnumerable()
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<MainArea>> GetAll(int skip, int take) => await _context.MainAreas
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .OrderBy(x => x.Name)
            .ToListAsync();

        public async Task<MainArea?> GetById(Guid? id) =>
            await _context.MainAreas
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<MainArea> Delete(Guid? id)
        {
            var model = await GetById(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<MainArea> Update(MainArea model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
        #endregion
    }
}