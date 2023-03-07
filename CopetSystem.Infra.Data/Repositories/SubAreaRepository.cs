using CopetSystem.Domain.Entities;
using CopetSystem.Domain.Interfaces;
using CopetSystem.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CopetSystem.Infra.Data.Repositories
{
    public class SubAreaRepository : ISubAreaRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public SubAreaRepository(ApplicationDbContext context) => _context = context;
        #endregion

        #region Public Methods
        public async Task<SubArea> Create(SubArea model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<SubArea?> GetByCode(string? code) => await _context.SubAreas
            .Where(x => x.Code == code && x.DeletedAt == null)
            .Include(x => x.Area)
            .Include(x => x.Area.MainArea)
            .ToAsyncEnumerable()
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<SubArea>> GetAll(int skip, int take) => await _context.SubAreas
            .Where(x => x.DeletedAt == null)
            .Skip(skip)
            .Take(take)
            .Include(x => x.Area)
            .Include(x => x.Area.MainArea)
            .AsAsyncEnumerable()
            .ToListAsync();

        public async Task<SubArea> GetById(Guid? id) =>
            await _context.SubAreas
                .Include(x => x.Area)
                .Include(x => x.Area.MainArea)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception($"Nenhuma Área encontrada para o id {id}");

        public async Task<SubArea> Delete(Guid? id)
        {
            var model = await this.GetById(id);
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<SubArea> Update(SubArea model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
        #endregion
    }
}

