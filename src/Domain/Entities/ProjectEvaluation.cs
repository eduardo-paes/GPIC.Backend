using Domain.Entities.Enums;
using Domain.Validation;

namespace Domain.Entities
{
    public class ProjectEvaluation
    {
        public Guid? Id { get; protected set; }

        #region Properties
        #region Informações Gerais da Avaliação
        private Guid? _projectId;
        public Guid? ProjectId
        {
            get => _projectId;
            set
            {
                EntityExceptionValidation.When(value is null,
                    ExceptionMessageFactory.Required(nameof(IsProductivityFellow)));
                _projectId = value;
            }
        }

        private bool? _isProductivityFellow;
        /// <summary>
        /// O professor é bolsista de Produtividade?
        /// </summary>
        public bool? IsProductivityFellow
        {
            get => _isProductivityFellow;
            set
            {
                EntityExceptionValidation.When(value is null,
                    ExceptionMessageFactory.Required(nameof(IsProductivityFellow)));
                _isProductivityFellow = value;
            }
        }

        private Guid? _submissionEvaluatorId;
        /// <summary>
        /// Id do avaliador que avaliou a submissão.
        /// </summary>
        public Guid? SubmissionEvaluatorId
        {
            get => _submissionEvaluatorId;
            set
            {
                EntityExceptionValidation.When(value is null,
                    ExceptionMessageFactory.Required(nameof(SubmissionEvaluatorId)));
                _submissionEvaluatorId = value;
            }
        }

        private DateTime? _submissionEvaluationDate;
        /// <summary>
        /// Data da avaliação da submissão.
        /// </summary>
        public DateTime? SubmissionEvaluationDate
        {
            get => _submissionEvaluationDate;
            set
            {
                EntityExceptionValidation.When(value is null,
                    ExceptionMessageFactory.Required(nameof(SubmissionEvaluationDate)));
                _submissionEvaluationDate = value.HasValue ? value.Value.ToUniversalTime() : null;
            }
        }

        private EProjectStatus? _submissionEvaluationStatus;
        /// <summary>
        /// Status da avaliação da submissão.
        /// </summary>
        public EProjectStatus? SubmissionEvaluationStatus
        {
            get => _submissionEvaluationStatus;
            set
            {
                EntityExceptionValidation.When(value is null,
                    ExceptionMessageFactory.Required(nameof(SubmissionEvaluationStatus)));
                _submissionEvaluationStatus = value;
            }
        }

        private string? _submissionEvaluationDescription;
        /// <summary>
        /// Nota da avaliação da submissão.
        /// </summary>
        public string? SubmissionEvaluationDescription
        {
            get => _submissionEvaluationDescription;
            set
            {
                EntityExceptionValidation.When(value is null,
                    ExceptionMessageFactory.Required(nameof(SubmissionEvaluationDescription)));
                _submissionEvaluationDescription = value;
            }
        }

        /// <summary>
        /// Id do avaliador que avaliou o recurso.
        /// </summary>
        public Guid? AppealEvaluatorId { get; set; }

        /// <summary>
        /// Data da avaliação do recurso.
        /// </summary>
        private DateTime? _appealEvaluationDate;
        public DateTime? AppealEvaluationDate
        {
            get { return _appealEvaluationDate; }
            set { _appealEvaluationDate = value.HasValue ? value.Value.ToUniversalTime() : null; }
        }

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
        private DateTime? _documentsEvaluationDate;
        public DateTime? DocumentsEvaluationDate
        {
            get { return _documentsEvaluationDate; }
            set { _documentsEvaluationDate = value.HasValue ? value.Value.ToUniversalTime() : null; }
        }

        /// <summary>
        /// Nota da avaliação da documentação do projeto.
        /// </summary>
        public string? DocumentsEvaluationDescription { get; set; }

        public virtual Project? Project { get; }
        public virtual User? SubmissionEvaluator { get; }
        public virtual User? AppealEvaluator { get; }
        public virtual User? DocumentsEvaluator { get; }
        #endregion Informações Gerais da Avaliação

        #region Critérios de Avaliação
        /// <summary>
        /// Pontuação Total (Índice AP).
        /// </summary>
        public double APIndex { get; set; }

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
        protected ProjectEvaluation() { }

        public ProjectEvaluation(Guid? projectId,
            bool? isProductivityFellow,
            Guid? submissionEvaluatorId,
            EProjectStatus? submissionEvaluationStatus,
            DateTime? submissionEvaluationDate,
            string? submissionEvaluationDescription,
            EQualification? qualification,
            EScore? projectProposalObjectives,
            EScore? academicScientificProductionCoherence,
            EScore? proposalMethodologyAdaptation,
            EScore? effectiveContributionToResearch,
            double apIndex)
        {
            ProjectId = projectId;
            IsProductivityFellow = isProductivityFellow;
            SubmissionEvaluatorId = submissionEvaluatorId;
            SubmissionEvaluationStatus = submissionEvaluationStatus;
            SubmissionEvaluationDate = submissionEvaluationDate;
            SubmissionEvaluationDescription = submissionEvaluationDescription;
            Qualification = qualification;
            ProjectProposalObjectives = projectProposalObjectives;
            AcademicScientificProductionCoherence = academicScientificProductionCoherence;
            ProposalMethodologyAdaptation = proposalMethodologyAdaptation;
            EffectiveContributionToResearch = effectiveContributionToResearch;
            APIndex = apIndex;
        }
        #endregion
    }
}