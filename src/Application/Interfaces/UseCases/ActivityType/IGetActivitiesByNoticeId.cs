using Application.Ports.Activity;

namespace Application.Interfaces.UseCases.ActivityType
{
    public interface IGetActivitiesByNoticeId
    {
        Task<IEnumerable<ActivityTypeOutput>> ExecuteAsync(Guid? id);
    }
}