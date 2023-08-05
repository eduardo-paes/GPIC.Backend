using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Project;
using Application.Ports.Project;
using Application.Validation;

namespace Application.UseCases.Project
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
            var project = await _projectRepository.GetByIdAsync(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.Project));

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
            project = await _projectRepository.UpdateAsync(project);

            // Mapeia entidade para output e retorna
            return _mapper.Map<ResumedReadProjectOutput>(project);
        }
    }
}