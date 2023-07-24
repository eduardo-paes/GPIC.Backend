using Adapters.Gateways.Activity;

namespace Adapters.Interfaces
{
    public interface IActivityPresenterController
    {
        Task<IEnumerable<ActivityTypeResponse>> GetActivitiesByNoticeId(Guid? noticeId);
        Task<IEnumerable<ActivityTypeResponse>> GetLastNoticeActivities();
    }
}