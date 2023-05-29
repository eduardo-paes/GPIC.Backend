using Domain.Entities.Enums;
using Domain.Validation;

namespace Domain.Entities
{
    public class ProjectEvaluation
    {
        public Guid? Id { get; protected set; }

        #region Properties
        #region Informações Gerais da Avaliação
        public Guid? ProjectId
        {
            get => ProjectId;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(IsProductivityFellow)));
        }

        /// <summary>
        /// O professor é bolsista de Produtividade?
        /// </summary>
        public bool? IsProductivityFellow
        {
            get => IsProductivityFellow;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(IsProductivityFellow)));
        }

        /// <summary>
        /// Id do avaliador que avaliou a submissão.
        /// </summary>
        public Guid? SubmissionEvaluatorId
        {
            get => SubmissionEvaluatorId;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(SubmissionEvaluatorId)));
        }

        /// <summary>
        /// Data da avaliação da submissão.
        /// </summary>
        public DateTime? SubmissionEvaluationDate
        {
            get => SubmissionEvaluationDate;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(SubmissionEvaluationDate)));
        }

        /// <summary>
        /// Status da avaliação da submissão.
        /// </summary>
        public EProjectStatus? SubmissionEvaluationStatus
        {
            get => SubmissionEvaluationStatus;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(SubmissionEvaluationStatus)));
        }

        /// <summary>
        /// Nota da avaliação da submissão.
        /// </summary>
        public string? SubmissionEvaluationDescription
        {
            get => SubmissionEvaluationDescription;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(SubmissionEvaluationDescription)));
        }

        /// <summary>
        /// Id do avaliador que avaliou o recurso.
        /// </summary>
        public Guid? AppealEvaluatorId { get; set; }

        /// <summary>
        /// Data da avaliação do recurso.
        /// </summary>
        public DateTime? AppealEvaluationDate { get; set; }

        /// <summary>
        /// Status da avaliação do recurso.
        /// </summary>
        public EProjectStatus? AppealEvaluationStatus { get; set; }

        /// <summary>
        /// Nota da avaliação do recurso.
        /// </summary>
        public string? AppealEvaluationDescription { get; set; }

        /// <summary>
        /// Id do avaliador que avaliou a documentação do projeto.
        /// </summary>
        public Guid? DocumentsEvaluatorId { get; set; }

        /// <summary>
        /// Data da avaliação da documentação do projeto.
        /// </summary>
        public DateTime? DocumentsEvaluationDate { get; set; }

        /// <summary>
        /// Nota da avaliação da documentação do projeto.
        /// </summary>
        public string? DocumentsEvaluationDescription { get; set; }

        public virtual Project? Project { get; }
        public virtual User? SubmissionEvaluator { get; }
        public virtual User? AppealEvaluator { get; }
        public virtual User? DocumentsEvaluator { get; }
        #endregion Informações Gerais da Avaliação

        #region (Resultados) Produção Científica - Trabalhos Publicados
        public int? FoundWorkType1
        {
            get => FoundWorkType1;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(FoundWorkType1)));
        }

        public int? FoundWorkType2
        {
            get => FoundWorkType2;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(FoundWorkType2)));
        }

        public int? FoundIndexedConferenceProceedings
        {
            get => FoundIndexedConferenceProceedings;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(FoundIndexedConferenceProceedings)));
        }

        public int? FoundNotIndexedConferenceProceedings
        {
            get => FoundNotIndexedConferenceProceedings;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(FoundNotIndexedConferenceProceedings)));
        }

        public int? FoundCompletedBook
        {
            get => FoundCompletedBook;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(FoundCompletedBook)));
        }

        public int? FoundOrganizedBook
        {
            get => FoundOrganizedBook;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(FoundOrganizedBook)));
        }

        public int? FoundBookChapters
        {
            get => FoundBookChapters;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(FoundBookChapters)));
        }

        public int? FoundBookTranslations
        {
            get => FoundBookTranslations;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(FoundBookTranslations)));
        }

        public int? FoundParticipationEditorialCommittees
        {
            get => FoundParticipationEditorialCommittees;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(FoundParticipationEditorialCommittees)));
        }
        #endregion

        #region (Resultados) Produção Artístca e Cultural - Produção Apresentada
        public int? FoundFullComposerSoloOrchestraAllTracks
        {
            get => FoundFullComposerSoloOrchestraAllTracks;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(FoundFullComposerSoloOrchestraAllTracks)));
        }

        public int? FoundFullComposerSoloOrchestraCompilation
        {
            get => FoundFullComposerSoloOrchestraCompilation;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(FoundFullComposerSoloOrchestraCompilation)));
        }

        public int? FoundChamberOrchestraInterpretation
        {
            get => FoundChamberOrchestraInterpretation;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(FoundChamberOrchestraInterpretation)));
        }

        public int? FoundIndividualAndCollectiveArtPerformances
        {
            get => FoundIndividualAndCollectiveArtPerformances;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(FoundIndividualAndCollectiveArtPerformances)));
        }

        public int? FoundScientificCulturalArtisticCollectionsCuratorship
        {
            get => FoundScientificCulturalArtisticCollectionsCuratorship;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(FoundScientificCulturalArtisticCollectionsCuratorship)));
        }
        #endregion

        #region (Resultados) Produção Técnica - Produtos Registrados
        public int? FoundPatentLetter
        {
            get => FoundPatentLetter;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(FoundPatentLetter)));
        }

        public int? FoundPatentDeposit
        {
            get => FoundPatentDeposit;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(FoundPatentDeposit)));
        }

        public int? FoundSoftwareRegistration
        {
            get => FoundSoftwareRegistration;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(FoundSoftwareRegistration)));
        }
        #endregion

        #region Critérios de Avaliação
        /// <summary>
        /// Pontuação Total (Índice AP).
        /// </summary>
        public int? APIndex
        {
            get => APIndex;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(APIndex)));
        }

        /// <summary>
        /// Titulação do Orientador.
        /// Doutor (2); Mestre (1).
        /// </summary>
        public EQualification? Qualification
        {
            get => Qualification;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(Qualification)));
        }

        /// <summary>
        /// Foco e clareza quanto aos objetivos da proposta de projeto a ser desenvolvido pelo aluno.
        /// Excelente (4); Bom (3); Regular (2); Fraco (1).
        /// </summary>
        public EScore? ProjectProposalObjectives
        {
            get => ProjectProposalObjectives;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(ProjectProposalObjectives)));
        }

        /// <summary>
        /// Coerência entre a produção acadêmico-científica do orientador e a proposta de projeto.
        /// Excelente (4); Bom (3); Regular (2); Fraco (1).
        /// </summary>
        public EScore? AcademicScientificProductionCoherence
        {
            get => AcademicScientificProductionCoherence;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(AcademicScientificProductionCoherence)));
        }

        /// <summary>
        /// Adequação da metodologia da proposta aos objetivos e ao cronograma de execução.
        /// Excelente (4); Bom (3); Regular (2); Fraco (1).
        /// </summary>
        public EScore? ProposalMethodologyAdaptation
        {
            get => ProposalMethodologyAdaptation;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(ProposalMethodologyAdaptation)));
        }

        /// <summary>
        /// Contribuição efetiva da proposta de projeto para formação em pesquisa do aluno.
        /// Excelente (4); Bom (3); Regular (2); Fraco (1).
        /// </summary>
        public EScore? EffectiveContributionToResearch
        {
            get => EffectiveContributionToResearch;
            set => EntityExceptionValidation.When(value is null,
                ExceptionMessageFactory.Required(nameof(EffectiveContributionToResearch)));
        }
        #endregion
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        public ProjectEvaluation() { }
        #endregion
    }
}