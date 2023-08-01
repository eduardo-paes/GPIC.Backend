using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Project;
using Domain.UseCases.Ports.Project;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Project
{
    public class SubmitProject : ISubmitProject
    {
        #region Global Scope
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public SubmitProject(IProjectRepository projectRepository,
            IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<ResumedReadProjectOutput> Execute(Guid? projectId)
        {
            // Verifica se Id foi informado.
            UseCaseException.NotInformedParam(projectId is null, nameof(projectId));

            // Verifica se o projeto existe
            Entities.Project project = await _projectRepository.GetById(projectId!.Value)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Project));

            // Verifica se edital está em fase de inscrição
            UseCaseException.BusinessRuleViolation(project.Notice?.RegistrationStartDate > DateTime.UtcNow || project.Notice?.RegistrationEndDate < DateTime.UtcNow,
                "O edital não está na fase de inscrição.");

            // Verifica se aluno está preenchido
            UseCaseException.BusinessRuleViolation(project.StudentId is null,
                "O projeto não possui aluno vinculado.");

            // Verifica se o projeto está aberto
            if (project.Status == EProjectStatus.Opened)
            {
                // Altera o status do projeto para submetido
                project.Status = EProjectStatus.Submitted;
                project.StatusDescription = EProjectStatus.Submitted.GetDescription();
                project.SubmissionDate = DateTime.UtcNow;

                // Salva alterações no banco de dados
                _ = await _projectRepository.Update(project);

                // Mapeia entidade para output e retorna
                return _mapper.Map<ResumedReadProjectOutput>(project);
            }

            throw UseCaseException.BusinessRuleViolation("Usuário não autorizado.");
        }
    }
}