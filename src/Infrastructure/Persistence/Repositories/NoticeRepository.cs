using System.Data.Entity;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class NoticeRepository : INoticeRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public NoticeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion Global Scope

        #region Public Methods
        public async Task<Notice> Create(Notice model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<Notice>> GetAll(int skip, int take)
        {
            return await _context.Notices
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .OrderByDescending(x => x.RegistrationStartDate)
            .ToListAsync();
        }

        public async Task<Notice?> GetById(Guid? id)
        {
            return await _context.Notices
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Notice> Delete(Guid? id)
        {
            Notice model = await GetById(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<Notice> Update(Notice model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Notice?> GetNoticeByPeriod(DateTime start, DateTime end)
        {
            DateTime startDate = start.ToUniversalTime();
            DateTime finalDate = end.ToUniversalTime();

            List<Notice> entities = await _context.Notices
                .Where(x => (x.RegistrationStartDate <= startDate && x.RegistrationEndDate >= finalDate)
                || (x.RegistrationStartDate <= finalDate && x.RegistrationEndDate >= finalDate)
                || (x.RegistrationStartDate <= startDate && x.RegistrationEndDate >= startDate))
                .AsAsyncEnumerable()
                .ToListAsync();
            return entities.FirstOrDefault();
        }
        #endregion Public Methods
    }
}