using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Project;
using Domain.UseCases.Ports.Project;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Project
{
    public class AppealProject : IAppealProject
    {
        #region Global Scope
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public AppealProject(IProjectRepository projectRepository,
            IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<ResumedReadProjectOutput> ExecuteAsync(Guid? projectId, string? appealDescription)
        {
            // Verifica se Id foi informado.
            UseCaseException.NotInformedParam(projectId is null, nameof(projectId));

            // Verifica se a descrição do recurso foi informada.
            UseCaseException.NotInformedParam(string.IsNullOrWhiteSpace(appealDescription),
                nameof(appealDescription));

            // Verifica se o projeto existe
            Entities.Project project = await _projectRepository.GetById(projectId!.Value)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Project));

            // Verifica se o projeto está em recurso
            if (project.Status == EProjectStatus.Rejected)
            {
                // Verifica se o edital está na fase de recurso.
                UseCaseException.BusinessRuleViolation(project.Notice?.AppealStartDate > DateTime.UtcNow
                    || project.Notice?.AppealEndDate < DateTime.UtcNow,
                    "O Edital não está na fase de Recurso.");

                // Altera o status do projeto para submetido
                project.Status = EProjectStatus.Evaluation;
                project.StatusDescription = EProjectStatus.Evaluation.GetDescription();
                project.AppealObservation = appealDescription;
                project.AppealDate = DateTime.UtcNow;

                // Salva alterações no banco de dados
                _ = await _projectRepository.Update(project);

                // Retorna o projeto
                return _mapper.Map<ResumedReadProjectOutput>(project);
            }

            throw UseCaseException.BusinessRuleViolation("O projeto não está em uma fase que permita recurso.");
        }
    }
}