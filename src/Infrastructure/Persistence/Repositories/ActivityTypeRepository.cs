using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ActivityTypeRepository : IActivityTypeRepository
    {
        #region Global Scope
        private readonly ApplicationDbContext _context;
        public ActivityTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion Global Scope

        public async Task<ActivityType> Create(ActivityType model)
        {
            _ = _context.Add(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ActivityType> Delete(Guid? id)
        {
            ActivityType model = await GetById(id)
                ?? throw new Exception($"Nenhum registro encontrado para o id ({id}) informado.");
            model.DeactivateEntity();
            return await Update(model);
        }

        public async Task<ActivityType?> GetById(Guid? id)
        {
            return await _context.ActivityTypes
                .Include(x => x.Notice)
                .IgnoreQueryFilters()
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception($"Nenhum tipo de atividade encontrado para o id {id}");
        }

        public async Task<IList<ActivityType>> GetByNoticeId(Guid? noticeId)
        {
            List<ActivityType> activityTypes = await _context.ActivityTypes
                .Include(x => x.Activities)
                .Where(x => x.NoticeId == noticeId)
                .ToListAsync()
                ?? throw new Exception("Nenhum tipo de atividade encontrado.");

            // Force loading of the Activities collection for each ActivityType
            foreach (ActivityType? activityType in activityTypes)
            {
                // Explicitly load the Activities collection
                await _context.Entry(activityType)
                    .Collection(x => x.Activities!)
                    .LoadAsync();
            }

            return activityTypes;
        }

        public async Task<IList<ActivityType>> GetLastNoticeActivities()
        {
            Guid lastNoticeId = await _context.Notices
                .AsAsyncEnumerable()
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => x.Id)
                .FirstOrDefaultAsync()
                ?? throw new Exception("Nenhum Edital encontrado.");
            return await GetByNoticeId(lastNoticeId);
        }

        public async Task<ActivityType> Update(ActivityType model)
        {
            _ = _context.Update(model);
            _ = await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<ActivityType>> GetAll(int skip, int take)
        {
            return await _context.ActivityTypes
            .Skip(skip)
            .Take(take)
            .AsAsyncEnumerable()
            .OrderBy(x => x.Name)
            .ToListAsync();
        }
    }
}