using AutoMapper;
using Domain.Contracts.Project;
using Domain.Contracts.ProjectEvaluation;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Interfaces.UseCases.ProjectEvaluation;
using Domain.Validation;

namespace Domain.UseCases.ProjectEvaluation
{
    public class EvaluateAppealProject : IEvaluateAppealProject
    {
        #region Global Scope
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly ITokenAuthenticationService _tokenAuthenticationService;
        private readonly IProjectEvaluationRepository _projectEvaluationRepository;
        public EvaluateAppealProject(IMapper mapper,
            IProjectRepository projectRepository,
            ITokenAuthenticationService tokenAuthenticationService,
            IProjectEvaluationRepository projectEvaluationRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _tokenAuthenticationService = tokenAuthenticationService;
            _projectEvaluationRepository = projectEvaluationRepository;
        }
        #endregion

        public async Task<DetailedReadProjectOutput> Execute(EvaluateAppealProjectInput input)
        {
            // Obtém informações do usuário logado.
            var user = _tokenAuthenticationService.GetUserAuthenticatedClaims();

            // Verifica se o usuário logado é um avaliador.
            if (user.Role != ERole.ADMIN.GetDescription() || user.Role != ERole.PROFESSOR.GetDescription())
                throw UseCaseException.BusinessRuleViolation("User is not an evaluator.");

            // Busca avaliação do projeto pelo Id.
            var projectEvaluation = await _projectEvaluationRepository.GetByProjectId(input.ProjectId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.ProjectEvaluation));

            // Recupera projeto pelo Id.
            var project = await _projectRepository.GetById(input.ProjectId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Project));

            // Verifica se o avaliador é o professor orientador do projeto.
            if (projectEvaluation.Project?.ProfessorId == user.Id)
                throw UseCaseException.BusinessRuleViolation("Evaluator is the project advisor.");

            // Verifica se o projeto está na fase de recurso.
            if (projectEvaluation.Project?.Status != EProjectStatus.Evaluation)
                throw UseCaseException.BusinessRuleViolation("Project is not in the evaluation phase.");

            // Verifica se o edital está na fase de recurso.
            if (projectEvaluation.Project.Notice?.AppealStartDate > DateTime.Now || projectEvaluation.Project.Notice?.AppealFinalDate < DateTime.Now)
                throw UseCaseException.BusinessRuleViolation("Notice isn't in the appeal stage.");

            // Verifica se o status da avaliação foi informado.
            if (input.AppealEvaluationStatus is null)
                throw UseCaseException.NotInformedParam(nameof(input.AppealEvaluationStatus));

            // Verifica se descrição da avaliação foi informada.
            if (string.IsNullOrEmpty(input.AppealEvaluationDescription))
                throw UseCaseException.NotInformedParam(nameof(input.AppealEvaluationDescription));

            // Atualiza a avaliação do recurso.
            projectEvaluation.AppealEvaluatorId = user.Id;
            projectEvaluation.AppealEvaluationDate = DateTime.Now;

            // Atualiza a descrição e o status da avaliação do recurso.
            projectEvaluation.AppealEvaluationDescription = input.AppealEvaluationDescription;
            projectEvaluation.AppealEvaluationStatus = (EProjectStatus)input.AppealEvaluationStatus;

            // Atualiza avaliação do projeto.
            await _projectEvaluationRepository.Update(projectEvaluation);

            // TODO: Se projeto foi aceito, adiciona prazo para envio da documentação.
            if ((EProjectStatus)input.AppealEvaluationStatus == EProjectStatus.Accepted)
            {
                project.Status = EProjectStatus.DocumentAnalysis;
                project.StatusDescription = EProjectStatus.DocumentAnalysis.GetDescription();
            }
            else
            {
                project.Status = EProjectStatus.Rejected;
                project.StatusDescription = EProjectStatus.Rejected.GetDescription();
            }

            // Atualiza projeto.
            var output = await _projectRepository.Update(project);

            // Mapeia dados de saída e retorna.
            return _mapper.Map<DetailedReadProjectOutput>(output);
        }
    }
}