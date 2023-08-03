using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.UseCases.Interfaces.ProjectEvaluation;
using Domain.UseCases.Ports.Project;
using Domain.UseCases.Ports.ProjectEvaluation;
using Domain.Validation;

namespace Domain.UseCases.Interactors.ProjectEvaluation
{
    public class EvaluateSubmissionProject : IEvaluateSubmissionProject
    {
        #region Global Scope
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly ITokenAuthenticationService _tokenAuthenticationService;
        private readonly IProjectActivityRepository _projectActivityRepository;
        private readonly IActivityTypeRepository _activityTypeRepository;
        private readonly IEmailService _emailService;
        private readonly IProjectEvaluationRepository _projectEvaluationRepository;
        public EvaluateSubmissionProject(IMapper mapper,
            IProjectRepository projectRepository,
            ITokenAuthenticationService tokenAuthenticationService,
            IProjectActivityRepository projectActivityRepository,
            IActivityTypeRepository activityTypeRepository,
            IEmailService emailService,
            IProjectEvaluationRepository projectEvaluationRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _tokenAuthenticationService = tokenAuthenticationService;
            _projectActivityRepository = projectActivityRepository;
            _activityTypeRepository = activityTypeRepository;
            _emailService = emailService;
            _projectEvaluationRepository = projectEvaluationRepository;
        }
        #endregion Global Scope

        public async Task<DetailedReadProjectOutput> ExecuteAsync(EvaluateSubmissionProjectInput input)
        {
            // Obtém informações do usuário logado.
            Ports.Auth.UserClaimsOutput user = _tokenAuthenticationService.GetUserAuthenticatedClaims();

            // Verifica se o usuário logado é um avaliador.
            UseCaseException.BusinessRuleViolation(user.Role != ERole.ADMIN.GetDescription()
                    || user.Role != ERole.PROFESSOR.GetDescription(),
                "O usuário não é um avaliador.");

            // Verifica se já existe alguma avaliação para o projeto.
            Entities.ProjectEvaluation? projectEvaluation = await _projectEvaluationRepository.GetByProjectId(input.ProjectId);
            UseCaseException.BusinessRuleViolation(projectEvaluation != null,
                "Projeto já avaliado.");

            // Busca projeto pelo Id.
            Entities.Project project = await _projectRepository.GetById(input.ProjectId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Project));

            // Verifica se o avaliador é o professor orientador do projeto.
            UseCaseException.BusinessRuleViolation(project.ProfessorId == user.Id,
                "Avaliador é o orientador do projeto.");

            // Verifica se o projeto está na fase de submissão.
            UseCaseException.BusinessRuleViolation(project.Status != EProjectStatus.Submitted,
                "O projeto não está em fase de submissão.");

            // Verifica se o edital está em fase de avaliação.
            UseCaseException.BusinessRuleViolation(project.Notice?.EvaluationStartDate > DateTime.UtcNow || project.Notice?.EvaluationEndDate < DateTime.UtcNow,
                "Edital encerrado.");

            // Verifica se o status da avaliação foi informado.
            UseCaseException.NotInformedParam(input.SubmissionEvaluationStatus is null,
                nameof(input.SubmissionEvaluationStatus));

            // Mapeia dados de entrada para entidade.
            projectEvaluation = new Entities.ProjectEvaluation(input.ProjectId,
                input.IsProductivityFellow,
                user.Id, // Id do avaliador logado.
                TryCastEnum<EProjectStatus>(input.SubmissionEvaluationStatus),
                DateTime.UtcNow,
                input.SubmissionEvaluationDescription,
                TryCastEnum<EQualification>(input.Qualification),
                TryCastEnum<EScore>(input.ProjectProposalObjectives),
                TryCastEnum<EScore>(input.AcademicScientificProductionCoherence),
                TryCastEnum<EScore>(input.ProposalMethodologyAdaptation),
                TryCastEnum<EScore>(input.EffectiveContributionToResearch),
                0);

            // Obtém atividades do Edital
            IList<Entities.ActivityType> noticeActivities = (IList<Entities.ActivityType>)await _activityTypeRepository.GetByNoticeId(project.Notice!.Id);

            // Obtém atividades do projeto
            IList<Entities.ProjectActivity> projectActivities = await _projectActivityRepository.GetByProjectId(project.Id);

            // Valida se todas as atividades do projeto foram informadas corretamente
            List<Entities.ProjectActivity> updateProjectActivities = new();
            foreach (Entities.ActivityType activityType in noticeActivities)
            {
                // Verifica se as atividades que o professor informou existem no edital 
                // e se todas as atividades do edital foram informadas.
                foreach (Entities.Activity activity in activityType.Activities!)
                {
                    // Verifica se professor informou valor para essa atividade do edital
                    Ports.ProjectActivity.EvaluateProjectActivityInput inputActivity = input.Activities!.FirstOrDefault(x => x.ActivityId == activity.Id)
                        ?? throw UseCaseException.BusinessRuleViolation($"Não foi informado valor para a atividade {activity.Name}.");

                    // Obtém atividade do projeto
                    Entities.ProjectActivity? updateProjectActivity = projectActivities.FirstOrDefault(x => x.ActivityId == activity.Id);

                    // Atualiza valores da entidade
                    updateProjectActivity!.FoundActivities = inputActivity.FoundActivities;

                    // Calcula pontuação da atividade
                    projectEvaluation.APIndex += updateProjectActivity.CalculatePoints();

                    // Atualiza atividade do projeto no banco de dados
                    _ = await _projectActivityRepository.Update(updateProjectActivity);
                }
            }

            // Calcula pontuação da avaliação
            projectEvaluation.CalculateFinalScore();

            // Adiciona avaliação do projeto.
            _ = await _projectEvaluationRepository.Create(projectEvaluation);

            // Se projeto foi aceito, adiciona prazo para envio da documentação.
            if (projectEvaluation.SubmissionEvaluationStatus == EProjectStatus.Accepted)
            {
                project.Status = EProjectStatus.Accepted;
                project.StatusDescription = EProjectStatus.Accepted.GetDescription();
            }
            else
            {
                project.Status = EProjectStatus.Rejected;
                project.StatusDescription = EProjectStatus.Rejected.GetDescription();
            }

            // Informa ao professor o resultado da avaliação.
            await _emailService.SendProjectNotificationEmailAsync(
                project.Professor!.User!.Email,
                project.Professor!.User!.Name,
                project.Title,
                project.StatusDescription,
                projectEvaluation.SubmissionEvaluationDescription);

            // Atualiza projeto.
            Entities.Project output = await _projectRepository.Update(project);

            // Mapeia dados de saída e retorna.
            return _mapper.Map<DetailedReadProjectOutput>(output);
        }

        /// <summary>
        /// Tenta converter um objeto para um tipo Enum.
        /// </summary>
        /// <param name="value">Valor a ser convertido.</param>
        /// <typeparam name="T">Tipo para o qual ser convertido.</typeparam>
        /// <returns>Objeto com tipo convertido.</returns>
        private static T TryCastEnum<T>(object? value)
        {
            try
            {
                UseCaseException.NotInformedParam(value is null, typeof(T).ToString());
                return (T)Enum.Parse(typeof(T), value?.ToString()!);
            }
            catch (Exception)
            {
                throw UseCaseException.BusinessRuleViolation($"Não foi possível converter o valor {value} para o tipo {typeof(T)}.");
            }
        }
    }
}