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
            UseCaseException.BusinessRuleViolation(user.Role != ERole.ADMIN.GetDescription()
                    || user.Role != ERole.PROFESSOR.GetDescription(),
                "O usuário não é um avaliador.");

            // Verifica se já existe alguma avaliação para o projeto.
            var projectEvaluation = await _projectEvaluationRepository.GetByProjectId(input.ProjectId);
            UseCaseException.BusinessRuleViolation(projectEvaluation != null,
                "Projeto já avaliado.");

            // Busca projeto pelo Id.
            var project = await _projectRepository.GetById(input.ProjectId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Project));

            // Verifica se o avaliador é o professor orientador do projeto.
            UseCaseException.BusinessRuleViolation(project.ProfessorId == user.Id,
                    "Avaliador é o orientador do projeto.");

            // Verifica se o projeto está na fase de submissão.
            UseCaseException.BusinessRuleViolation(project.Status != EProjectStatus.Submitted,
                "O projeto não está em fase de submissão.");

            // Verifica se o edital ainda está aberto.
            UseCaseException.BusinessRuleViolation(project.Notice?.StartDate > DateTime.UtcNow || project.Notice?.FinalDate < DateTime.UtcNow,
                "Edital encerrado.");

            // Verifica se o status da avaliação foi informado.
            UseCaseException.NotInformedParam(input.SubmissionEvaluationStatus is null,
                nameof(input.SubmissionEvaluationStatus));

            // Verifica se a descrição da avaliação foi informada.
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.SubmissionEvaluationDescription),
                nameof(input.SubmissionEvaluationDescription));

            // Mapeia dados de entrada para entidade.
            projectEvaluation = new Entities.ProjectEvaluation(input.ProjectId,
                input.IsProductivityFellow,
                user.Id, // Id do avaliador logado.
                TryCastEnum<EProjectStatus>(input.SubmissionEvaluationStatus),
                DateTime.UtcNow,
                input.SubmissionEvaluationDescription,
                input.FoundWorkType1,
                input.FoundWorkType2,
                input.FoundIndexedConferenceProceedings,
                input.FoundNotIndexedConferenceProceedings,
                input.FoundCompletedBook,
                input.FoundOrganizedBook,
                input.FoundBookChapters,
                input.FoundBookTranslations,
                input.FoundParticipationEditorialCommittees,
                input.FoundFullComposerSoloOrchestraAllTracks,
                input.FoundFullComposerSoloOrchestraCompilation,
                input.FoundChamberOrchestraInterpretation,
                input.FoundIndividualAndCollectiveArtPerformances,
                input.FoundScientificCulturalArtisticCollectionsCuratorship,
                input.FoundPatentLetter,
                input.FoundPatentDeposit,
                input.FoundSoftwareRegistration,
                TryCastEnum<EQualification>(input.Qualification),
                TryCastEnum<EScore>(input.ProjectProposalObjectives),
                TryCastEnum<EScore>(input.AcademicScientificProductionCoherence),
                TryCastEnum<EScore>(input.ProposalMethodologyAdaptation),
                TryCastEnum<EScore>(input.EffectiveContributionToResearch));

            // Adiciona avaliação do projeto.
            await _projectEvaluationRepository.Create(projectEvaluation);

            // Se projeto foi aceito, adiciona prazo para envio da documentação.
            if (projectEvaluation.SubmissionEvaluationStatus == EProjectStatus.Accepted)
            {
                project.Status = EProjectStatus.DocumentAnalysis;
                project.StatusDescription = EProjectStatus.DocumentAnalysis.GetDescription();
                project.SendingDocumentationDeadline = DateTime.UtcNow.AddDays(project.Notice?.SendingDocumentationDeadline ?? 30);
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