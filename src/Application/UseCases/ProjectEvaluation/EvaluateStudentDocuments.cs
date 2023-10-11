using Application.Interfaces.UseCases.ProjectEvaluation;
using Application.Ports.Project;
using Application.Ports.ProjectEvaluation;
using Application.Validation;
using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.UseCases.ProjectEvaluation
{
    public class EvaluateStudentDocuments : IEvaluateStudentDocuments
    {
        #region Global Scope
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IEmailService _emailService;
        private readonly ITokenAuthenticationService _tokenAuthenticationService;
        private readonly IProjectEvaluationRepository _projectEvaluationRepository;
        public EvaluateStudentDocuments(IMapper mapper,
            IProjectRepository projectRepository,
            IEmailService emailService,
            ITokenAuthenticationService tokenAuthenticationService,
            IProjectEvaluationRepository projectEvaluationRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _emailService = emailService;
            _tokenAuthenticationService = tokenAuthenticationService;
            _projectEvaluationRepository = projectEvaluationRepository;
        }
        #endregion Global Scope

        public async Task<DetailedReadProjectOutput> ExecuteAsync(EvaluateStudentDocumentsInput input)
        {
            // Obtém informações do usuário logado.
            var userClaims = _tokenAuthenticationService.GetUserAuthenticatedClaims();

            // Obtém id do usuário e id de acordo com perfil logado
            var userClaim = userClaims!.Values.FirstOrDefault();
            var actorId = userClaims.Keys.FirstOrDefault();

            // Verifica se o usuário logado é um avaliador.
            UseCaseException.BusinessRuleViolation(userClaim!.Role != ERole.ADMIN || userClaim.Role != ERole.PROFESSOR,
                "O usuário não é um avaliador.");

            // Verifica se o status da avaliação foi informado.
            UseCaseException.NotInformedParam(input.IsDocumentsApproved is null,
                nameof(input.IsDocumentsApproved));

            // Verifica se descrição da avaliação foi informada.
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.DocumentsEvaluationDescription),
                nameof(input.DocumentsEvaluationDescription));

            // Busca avaliação do projeto pelo Id.
            var projectEvaluation = await _projectEvaluationRepository.GetByProjectIdAsync(input.ProjectId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.ProjectEvaluation));

            // Verifica se o avaliador é o professor orientador do projeto.
            UseCaseException.BusinessRuleViolation(projectEvaluation.Project?.ProfessorId == userClaim.Id,
                "Avaliador é o orientador do projeto.");

            // Verifica se o projeto está na fase de avaliação da documentação.
            UseCaseException.BusinessRuleViolation(projectEvaluation.Project?.Status != EProjectStatus.DocumentAnalysis,
                "Projeto não está em fase de avaliação da documentação.");

            // Verifica se o edital está na fase de avaliação da documentação.
            UseCaseException.BusinessRuleViolation(projectEvaluation?.Project?.Notice?.SendingDocsStartDate > DateTime.UtcNow
                || projectEvaluation?.Project?.Notice?.SendingDocsEndDate < DateTime.UtcNow,
                "O edital não está na fase de avaliação da documentação.");

            // Recupera projeto pelo Id.
            var project = await _projectRepository.GetByIdAsync(input.ProjectId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.Project));

            // Atualiza a avaliação do recurso.
            projectEvaluation!.DocumentsEvaluatorId = userClaim.Id;
            projectEvaluation.DocumentsEvaluationDate = DateTime.UtcNow;

            // Atualiza a descrição e o status da avaliação da documentação.
            projectEvaluation.DocumentsEvaluationDescription = input.DocumentsEvaluationDescription;

            // Atualiza avaliação do projeto.
            _ = await _projectEvaluationRepository.UpdateAsync(projectEvaluation);

            // Atualiza status do projeto de acordo com a avaliação da documentação.
            if ((bool)input.IsDocumentsApproved!)
            {
                project.Status = EProjectStatus.Started;
                project.StatusDescription = EProjectStatus.Started.GetDescription();
            }
            else
            {
                project.Status = EProjectStatus.Pending;
                project.StatusDescription = EProjectStatus.Pending.GetDescription();
            }

            // Informa ao professor o resultado da avaliação.
            await _emailService.SendProjectNotificationEmailAsync(
                project.Professor!.User!.Email,
                project.Professor!.User!.Name,
                project.Title,
                project.StatusDescription,
                projectEvaluation.DocumentsEvaluationDescription);

            // Atualiza projeto.
            var output = await _projectRepository.UpdateAsync(project);

            // Mapeia dados de saída e retorna.
            return _mapper.Map<DetailedReadProjectOutput>(output);
        }
    }
}