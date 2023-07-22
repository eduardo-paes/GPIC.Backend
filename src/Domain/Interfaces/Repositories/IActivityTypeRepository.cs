using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface IActivityTypeRepository : IGenericCRUDRepository<ActivityType>
    {
        Task<IEnumerable<ActivityType>> GetByNoticeId(Guid? noticeId);
        Task<IEnumerable<ActivityType>> GetLastNoticeActivities();
    }
}