using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Ports.ProjectActivity;
using Application.Interfaces.UseCases.Project;

namespace Application.UseCases.ActivityType
{
    public class GetActivitiesByProjectId : IGetActivitiesByProjectId
    {
        private readonly IProjectActivityRepository _projectActivityRepository;
        private readonly IMapper _mapper;
        public GetActivitiesByProjectId(IProjectActivityRepository projectActivityRepository, IMapper mapper)
        {
            _projectActivityRepository = projectActivityRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DetailedReadProjectActivityOutput>> ExecuteAsync(Guid? projectId)
        {
            // Obtém os tipos de atividades do edital
            var projectActivities = await _projectActivityRepository.GetByProjectIdAsync(projectId);

            // Lista de tipos de atividades para o output
            List<DetailedReadProjectActivityOutput> projectActivitiesOutput = new();

            // Se não houver tipos de atividades, retorna a lista vazia
            if (projectActivities == null || projectActivities.Count == 0)
                return projectActivitiesOutput;

            // Mapeia os tipos de atividades para o output
            foreach (var projectActivity in projectActivities)
            {
                // Adiciona o tipo de atividade ao output
                projectActivitiesOutput.Add(new DetailedReadProjectActivityOutput
                {
                    Id = projectActivity.Id,
                    ActivityId = projectActivity.ActivityId,
                    ProjectId = projectActivity.ProjectId,
                    InformedActivities = projectActivity.InformedActivities,
                    DeletedAt = projectActivity.DeletedAt,
                    FoundActivities = projectActivity.FoundActivities
                });
            }

            return projectActivitiesOutput;
        }
    }
}