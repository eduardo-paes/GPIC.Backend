using AutoMapper;
using Domain.Contracts.Project;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases.Project;
using Domain.Validation;

namespace Domain.UseCases.Project
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
        #endregion

        public async Task<ResumedReadProjectOutput> Execute(Guid? id, string? observation)
        {
            // Verifica se o projeto existe
            var project = await _projectRepository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Project));

            // Verifica se o projeto já não foi cancelado ou está encerrado
            UseCaseException.BusinessRuleViolation(project.Status != EProjectStatus.Canceled || project.Status != EProjectStatus.Closed,
                "Project already canceled or terminated.");

            // Atualiza informações de cancelamento do projeto
            project.Status = EProjectStatus.Canceled;
            project.StatusDescription = EProjectStatus.Canceled.GetDescription();
            project.CancellationReason = observation;
            project.CancellationDate = DateTime.Now;

            // Atualiza projeto
            project = await _projectRepository.Update(project);

            // Mapeia entidade para output e retorna
            return _mapper.Map<ResumedReadProjectOutput>(project);
        }
    }
}