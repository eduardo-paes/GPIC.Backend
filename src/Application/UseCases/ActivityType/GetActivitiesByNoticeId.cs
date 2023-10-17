using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Ports.Activity;
using Application.Interfaces.UseCases.ActivityType;

namespace Application.UseCases.ActivityType
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

        public async Task<IEnumerable<ActivityTypeOutput>> ExecuteAsync(Guid? noticeId)
        {
            // Obtém os tipos de atividades do edital
            var activityTypes = await _activityTypeRepository.GetByNoticeIdAsync(noticeId);

            // Lista de tipos de atividades para o output
            List<ActivityTypeOutput> activityTypesOutput = new();

            // Se não houver tipos de atividades, retorna a lista vazia
            if (activityTypes == null)
                return activityTypesOutput;

            // Mapeia os tipos de atividades para o output
            _ = _mapper.Map<IEnumerable<ActivityTypeOutput>>(activityTypes);

            // Mapeia os tipos de atividades para o output
            foreach (var activityType in activityTypes)
            {
                // Mapeia as atividades para o output
                List<ActivityOutput> activitiesOutput = new();
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