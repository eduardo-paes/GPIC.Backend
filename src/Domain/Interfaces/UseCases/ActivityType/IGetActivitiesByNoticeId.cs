using Domain.Contracts.Activity;

namespace Domain.Interfaces.UseCases.ActivityType;
public interface IGetActivitiesByNoticeId
{
    Task<IEnumerable<ActivityTypeOutput>> GetActivitiesByNoticeId(Guid? id);
}