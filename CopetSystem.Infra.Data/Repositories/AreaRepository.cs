// using System.Data.Entity;
using CopetSystem.Domain.Entities;
using CopetSystem.Domain.Interfaces;
using CopetSystem.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CopetSystem.Infra.Data.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public AreaRepository(ApplicationDbContext context) => _context = context;

        public async Task<Area> Create(Area model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }
        #endregion

        #region Public Methods
        public async Task<Area?> GetByCode(string? code) => await _context.Areas
            .Where(x => x.Code == code && x.DeletedAt == null)
            .ToAsyncEnumerable()
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<Area>> GetAll(int skip, int take) => await _context.Areas
            .Where(x => x.DeletedAt == null)
            .Skip(skip)
            .Take(take)
            .Include(x => x.MainArea)
            .AsAsyncEnumerable()
            .ToListAsync();

        public async Task<Area> GetById(Guid? id) =>
            await _context.Areas
                .Include(x => x.MainArea)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception($"Nenhuma Área encontrada para o id {id}");


        public async Task<Area> Delete(Guid? id)
        {
            var model = await this.GetById(id);
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<Area> Update(Area model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
        #endregion
    }
}

