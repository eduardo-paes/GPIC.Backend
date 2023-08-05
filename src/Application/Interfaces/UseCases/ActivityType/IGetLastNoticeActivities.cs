using Application.Ports.Activity;

namespace Application.Interfaces.UseCases.ActivityType
{
    public interface IGetLastNoticeActivities
    {
        Task<IEnumerable<ActivityTypeOutput>> ExecuteAsync();
    }
}