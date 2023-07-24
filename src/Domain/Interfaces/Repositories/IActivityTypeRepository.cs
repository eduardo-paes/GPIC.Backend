using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface IActivityTypeRepository : IGenericCRUDRepository<ActivityType>
    {
        Task<IList<ActivityType>> GetByNoticeId(Guid? noticeId);
        Task<IList<ActivityType>> GetLastNoticeActivities();
    }
}