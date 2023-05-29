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
    public class EvaluateSubmissionProject : IEvaluateSubmissionProject
    {
        #region Global Scope
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly ITokenAuthenticationService _tokenAuthenticationService;
        private readonly IProjectEvaluationRepository _projectEvaluationRepository;
        public EvaluateSubmissionProject(IMapper mapper,
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

        public async Task<DetailedReadProjectOutput> Execute(EvaluateSubmissionProjectInput input)
        {
            // Obtém informações do usuário logado.
            var user = _tokenAuthenticationService.GetUserAuthenticatedClaims();

            // Verifica se o usuário logado é um avaliador.
            if (user.Role != ERole.ADMIN.GetDescription() || user.Role != ERole.PROFESSOR.GetDescription())
                throw UseCaseException.BusinessRuleViolation("User is not an evaluator.");

            // Verifica se já existe alguma avaliação para o projeto.
            var projectEvaluation = await _projectEvaluationRepository.GetByProjectId(input.ProjectId);
            if (projectEvaluation != null)
                throw UseCaseException.BusinessRuleViolation("Project already evaluated.");

            // Busca projeto pelo Id.
            var project = await _projectRepository.GetById(input.ProjectId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Project));

            // Verifica se o avaliador é o professor orientador do projeto.
            if (project.ProfessorId == user.Id)
                throw UseCaseException.BusinessRuleViolation("Evaluator is the project advisor.");

            // Verifica se o projeto está na fase de submissão.
            if (project.Status != EProjectStatus.Submitted)
                throw UseCaseException.BusinessRuleViolation("Project is not in the submission phase.");

            // Verifica se o edital ainda está aberto.
            if (project.Notice?.StartDate > DateTime.Now || project.Notice?.FinalDate < DateTime.Now)
                throw UseCaseException.BusinessRuleViolation("Notice is closed.");

            // Verifica se o status do projeto foi informado.
            if (input.SubmissionEvaluationStatus == null)
                throw UseCaseException.NotInformedParam(nameof(input.SubmissionEvaluationStatus));

            // Verifica se descrição do projeto foi informada.
            if (string.IsNullOrEmpty(input.SubmissionEvaluationDescription))
                throw UseCaseException.NotInformedParam(nameof(input.SubmissionEvaluationDescription));

            // Atribui informações de avaliação.
            input.SubmissionEvaluationDate = DateTime.Now;
            input.SubmissionEvaluatorId = user.Id;

            // Mapeia dados de entrada para entidade.
            var projectEvaluationEntity = _mapper.Map<Entities.ProjectEvaluation>(input);

            // Adiciona avaliação do projeto.
            var output = await _projectEvaluationRepository.Create(projectEvaluationEntity);

            // Mapeia dados de saída e retorna.
            return _mapper.Map<DetailedReadProjectOutput>(output);
        }
    }
}