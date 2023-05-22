using System.Data.Entity;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class CampusRepository : ICampusRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public CampusRepository(ApplicationDbContext context) => _context = context;
        #endregion

        #region Public Methods
        public async Task<Campus> Create(Campus model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<Campus>> GetAll(int skip, int take) => await _context.Campuses
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .OrderBy(x => x.Name)
            .ToListAsync();

        public async Task<Campus?> GetById(Guid? id) =>
            await _context.Campuses
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Campus> Delete(Guid? id)
        {
            var model = await GetById(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<Campus> Update(Campus model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Campus?> GetCampusByName(string name)
        {
            string loweredName = name.ToLower();
            var entities = await _context.Campuses
                .Where(x => x.Name!.ToLower() == loweredName)
                .AsAsyncEnumerable()
                .ToListAsync();
            return entities.FirstOrDefault();
        }
        #endregion
    }
}