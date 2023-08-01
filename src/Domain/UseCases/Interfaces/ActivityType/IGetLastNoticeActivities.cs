using Domain.UseCases.Ports.Activity;

namespace Domain.UseCases.Interfaces.ActivityType
{
    public interface IGetLastNoticeActivities
    {
        Task<IEnumerable<ActivityTypeOutput>> GetLastNoticeActivities();
    }
}