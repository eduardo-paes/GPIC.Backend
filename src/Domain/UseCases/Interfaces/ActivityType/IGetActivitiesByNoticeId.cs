using Domain.UseCases.Ports.Activity;

namespace Domain.UseCases.Interfaces.ActivityType
{
    public interface IGetActivitiesByNoticeId
    {
        Task<IEnumerable<ActivityTypeOutput>> GetActivitiesByNoticeId(Guid? id);
    }
}