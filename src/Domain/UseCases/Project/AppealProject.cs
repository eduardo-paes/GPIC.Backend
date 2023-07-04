using AutoMapper;
using Domain.Contracts.Project;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases.Project;
using Domain.Validation;

namespace Domain.UseCases.Project
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
        #endregion

        public async Task<ResumedReadProjectOutput> Execute(Guid? projectId, string? appealDescription)
        {
            // Verifica se Id foi informado.
            UseCaseException.NotInformedParam(projectId is null, nameof(projectId));

            // Verifica se a descrição do recurso foi informada.
            UseCaseException.NotInformedParam(string.IsNullOrWhiteSpace(appealDescription),
                nameof(appealDescription));

            // Verifica se o projeto existe
            var project = await _projectRepository.GetById(projectId!.Value)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Project));

            // Verifica se o projeto está em recurso
            if (project.Status == EProjectStatus.Rejected)
            {
                // Altera o status do projeto para submetido
                project.Status = EProjectStatus.Evaluation;
                project.StatusDescription = EProjectStatus.Evaluation.GetDescription();
                project.AppealObservation = appealDescription;
                project.ResubmissionDate = DateTime.Now;

                // Salva alterações no banco de dados
                await _projectRepository.Update(project);

                // Retorna o projeto
                return _mapper.Map<ResumedReadProjectOutput>(project);
            }
            else
            {
                throw UseCaseException.BusinessRuleViolation("The project is not at a stage that allows appeal.");
            }
        }
    }
}