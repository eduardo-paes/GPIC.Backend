using Adapters.Gateways.Activity;
using Adapters.Interfaces;
using AutoMapper;
using Domain.Interfaces.UseCases.ActivityType;

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
            var activityTypes = await _getActivitiesByNoticeId.GetActivitiesByNoticeId(noticeId);
            return _mapper.Map<IEnumerable<ActivityTypeResponse>>(activityTypes);
        }

        public async Task<IEnumerable<ActivityTypeResponse>> GetLastNoticeActivities()
        {
            var activityTypes = await _getLastNoticeActivities.GetLastNoticeActivities();
            return _mapper.Map<IEnumerable<ActivityTypeResponse>>(activityTypes);
        }
    }
}