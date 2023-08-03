using Adapters.Gateways.Activity;
using Adapters.Interfaces;
using AutoMapper;
using Domain.UseCases.Interfaces.ActivityType;

namespace Adapters.PresenterController
{
    public class ActivityPresenterController : IActivityPresenterController
    {
        private readonly IGetActivitiesByNoticeId _getActivitiesByNoticeId;
        private readonly IGetLastNoticeActivities _getLastNoticeActivities;
        private readonly IMapper _mapper;
        public ActivityPresenterController(
            IGetActivitiesByNoticeId getActivitiesByNoticeId,
            IGetLastNoticeActivities getLastNoticeActivities,
            IMapper mapper)
        {
            _getActivitiesByNoticeId = getActivitiesByNoticeId;
            _getLastNoticeActivities = getLastNoticeActivities;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActivityTypeResponse>> GetActivitiesByNoticeId(Guid? noticeId)
        {
            IEnumerable<Domain.UseCases.Ports.Activity.ActivityTypeOutput> activityTypes = await _getActivitiesByNoticeId.ExecuteAsync(noticeId);
            return _mapper.Map<IEnumerable<ActivityTypeResponse>>(activityTypes);
        }

        public async Task<IEnumerable<ActivityTypeResponse>> GetLastNoticeActivities()
        {
            IEnumerable<Domain.UseCases.Ports.Activity.ActivityTypeOutput> activityTypes = await _getLastNoticeActivities.ExecuteAsync();
            return _mapper.Map<IEnumerable<ActivityTypeResponse>>(activityTypes);
        }
    }
}