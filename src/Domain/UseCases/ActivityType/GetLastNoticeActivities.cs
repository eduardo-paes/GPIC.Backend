using AutoMapper;
using Domain.Contracts.Activity;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases.ActivityType;

namespace Domain.UseCases.ActivityType
{
    public class GetLastNoticeActivities : IGetLastNoticeActivities
    {
        private readonly IActivityTypeRepository _activityTypeRepository;
        private readonly IMapper _mapper;
        public GetLastNoticeActivities(IActivityTypeRepository activityTypeRepository, IMapper mapper)
        {
            _activityTypeRepository = activityTypeRepository;
            _mapper = mapper;
        }
        async Task<IEnumerable<ActivityTypeOutput>> IGetLastNoticeActivities.GetLastNoticeActivities()
        {
            // Obtém os tipos de atividades do último edital
            var activityTypes = await _activityTypeRepository.GetLastNoticeActivities();

            // Mapeia os tipos de atividades para o output
            var output = _mapper.Map<IEnumerable<ActivityTypeOutput>>(activityTypes);

            // return output;

            // Mapeia os tipos de atividades para o output
            var activityTypesOutput = new List<ActivityTypeOutput>();
            foreach (var type in activityTypes)
            {
                // Mapeia as atividades para o output
                var activitiesOutput = new List<ActivityOutput>();
                foreach (var activity in type.Activities!)
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
                    Id = type.Id,
                    Name = type.Name,
                    Unity = type.Unity,
                    DeletedAt = type.DeletedAt,
                    Activities = activitiesOutput
                });
            }

            // Retorna os tipos de atividades do último edital
            return activityTypesOutput;
        }
    }
}