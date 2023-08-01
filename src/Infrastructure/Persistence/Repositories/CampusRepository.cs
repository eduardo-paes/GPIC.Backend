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
        public async Task<Campus> Create(Campus model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<Campus>> GetAll(int skip, int take)
        {
            return await _context.Campuses
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .OrderBy(x => x.Name)
            .ToListAsync();
        }

        public async Task<Campus?> GetById(Guid? id)
        {
            return await _context.Campuses
            .IgnoreQueryFilters()
            .AsAsyncEnumerable()
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Campus> Delete(Guid? id)
        {
            Campus model = await GetById(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<Campus> Update(Campus model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Campus?> GetCampusByName(string name)
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