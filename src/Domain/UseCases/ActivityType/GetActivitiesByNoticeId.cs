using AutoMapper;
using Domain.Contracts.Activity;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases.ActivityType;

namespace Domain.UseCases.ActivityType
{
    public class GetActivitiesByNoticeId : IGetActivitiesByNoticeId
    {
        private readonly IActivityTypeRepository _activityTypeRepository;
        private readonly IMapper _mapper;
        public GetActivitiesByNoticeId(IActivityTypeRepository activityTypeRepository, IMapper mapper)
        {
            _activityTypeRepository = activityTypeRepository;
            _mapper = mapper;
        }

        async Task<IEnumerable<ActivityTypeOutput>> IGetActivitiesByNoticeId.GetActivitiesByNoticeId(Guid? id)
        {
            // Obtém os tipos de atividades do edital
            var activityTypes = await _activityTypeRepository.GetByNoticeId(id);

            // Mapeia os tipos de atividades para o output
            var output = _mapper.Map<IEnumerable<ActivityTypeOutput>>(activityTypes);

            // return output;

            // Mapeia os tipos de atividades para o output
            var activityTypesOutput = new List<ActivityTypeOutput>();
            foreach (var activityType in activityTypes)
            {
                // Mapeia as atividades para o output
                var activitiesOutput = new List<ActivityOutput>();
                foreach (var activity in activityType.Activities!)
                {
                    activitiesOutput.Add(new ActivityOutput
                    {
                        Id = activity.Id,
                        Name = activity.Name,
                        Points = activity.Points,
                        Limits = activity.Limits,
                        DeletedAt = activity.DeletedAt
                    });
                }

                // Adiciona o tipo de atividade ao output
                activityTypesOutput.Add(new ActivityTypeOutput
                {
                    Id = activityType.Id,
                    Name = activityType.Name,
                    Unity = activityType.Unity,
                    DeletedAt = activityType.DeletedAt,
                    Activities = activitiesOutput
                });
            }

            return activityTypesOutput;
        }
    }
}