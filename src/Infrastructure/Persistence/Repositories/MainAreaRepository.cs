using System.Data.Entity;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class MainAreaRepository : IMainAreaRepository
    {
        #region Global Scope
        private readonly AdaptersDbContext _context;
        public MainAreaRepository(AdaptersDbContext context) => _context = context;
        #endregion

        #region Public Methods
        public async Task<MainArea> Create(MainArea model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<MainArea?> GetByCode(string? code) => await _context.MainAreas
            .Where(x => x.Code == code && x.DeletedAt == null)
            .ToAsyncEnumerable()
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<MainArea>> GetAll(int skip, int take) => await _context.MainAreas
            .Where(x => x.DeletedAt == null)
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .ToListAsync();

        public async Task<MainArea> GetById(Guid? id) =>
            await _context.MainAreas.FindAsync(id)
                ?? throw new Exception($"Nenhuma Área Principal encontrada para o id {id}");

        public async Task<MainArea> Delete(Guid? id)
        {
            var model = await this.GetById(id);
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

