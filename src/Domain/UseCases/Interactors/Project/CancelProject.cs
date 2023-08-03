using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Project;
using Domain.UseCases.Ports.Project;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Project
{
    public class CancelProject : ICancelProject
    {
        #region Global Scope
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public CancelProject(IProjectRepository projectRepository,
            IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<ResumedReadProjectOutput> ExecuteAsync(Guid? id, string? observation)
        {
            // Verifica se o projeto existe
            Entities.Project project = await _projectRepository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Project));

            // Verifica se o projeto já não foi cancelado ou está encerrado
            UseCaseException.BusinessRuleViolation(
                project.Status is EProjectStatus.Canceled or EProjectStatus.Closed,
                "Projeto já cancelado ou encerrado.");

            // Atualiza informações de cancelamento do projeto
            project.Status = EProjectStatus.Canceled;
            project.StatusDescription = EProjectStatus.Canceled.GetDescription();
            project.CancellationReason = observation;
            project.CancellationDate = DateTime.UtcNow;

            // Atualiza projeto
            project = await _projectRepository.Update(project);

            // Mapeia entidade para output e retorna
            return _mapper.Map<ResumedReadProjectOutput>(project);
        }
    }
}