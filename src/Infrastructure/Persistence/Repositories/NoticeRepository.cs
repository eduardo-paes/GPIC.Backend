using System.Data.Entity;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class NoticeRepository : INoticeRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public NoticeRepository(ApplicationDbContext context) => _context = context;
        #endregion

        #region Public Methods
        public async Task<Notice> Create(Notice model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<Notice>> GetAll(int skip, int take) => await _context.Notices
            .Where(x => x.DeletedAt == null)
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .ToListAsync();

        public async Task<Notice> GetById(Guid? id) =>
            await _context.Notices.FindAsync(id)
                ?? throw new Exception($"Nenhuma Área Principal encontrada para o id {id}");

        public async Task<Notice> Delete(Guid? id)
        {
            var model = await this.GetById(id);
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<Notice> Update(Notice model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Notice?> GetNoticeByPeriod(DateTime start, DateTime end)
        {
            return await _context.Notices
                .FirstOrDefaultAsync(x => (x.StartDate <= start && x.FinalDate >= end)
                || (x.StartDate <= end && x.FinalDate >= end)
                || (x.StartDate <= start && x.FinalDate >= start));
        }
        #endregion
    }
}

