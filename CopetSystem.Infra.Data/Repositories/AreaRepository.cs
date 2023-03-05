using System;
using System.Data.Entity;
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
        public async Task<Area> GetByCode(string? code) => await _context.Areas
            .FirstOrDefaultAsync(x => x.Code == code && x.DeletedAt == null);

        public async Task<IEnumerable<Area>> GetAll() => await _context.Areas
            .Where(x => x.DeletedAt == null).ToListAsync();

        public async Task<Area> GetById(Guid? id) =>
            await _context.Areas.FindAsync(id)
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

