using Domain.Contracts.Activity;

namespace Domain.Interfaces.UseCases.ActivityType
{
    public interface IGetLastNoticeActivities
    {
        Task<IEnumerable<ActivityTypeOutput>> GetLastNoticeActivities();
    }
}