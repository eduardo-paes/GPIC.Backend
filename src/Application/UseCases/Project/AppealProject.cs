using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Project;
using Application.Ports.Project;
using Application.Validation;

namespace Application.UseCases.Project
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
            var project = await _projectRepository.GetByIdAsync(projectId!.Value)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.Project));

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
                _ = await _projectRepository.UpdateAsync(project);

                // Retorna o projeto
                return _mapper.Map<ResumedReadProjectOutput>(project);
            }

            throw UseCaseException.BusinessRuleViolation("O projeto não está em uma fase que permita recurso.");
        }
    }
}