using Domain.Entities.Enums;
using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    public class ProjectEvaluation : Entity
    {
        #region Properties
        #region Informações Gerais da Avaliação
        private Guid? _projectId;
        public Guid? ProjectId
        {
            get => _projectId;
            set
            {
                EntityExceptionValidation.When(value is null,
                    ExceptionMessageFactory.Required(nameof(ProjectId)));
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
                _submissionEvaluationDate = value;
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
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
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
            set { _appealEvaluationDate = value; }
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
            set { _documentsEvaluationDate = value; }
        }

        /// <summary>
        /// Nota da avaliação da documentação do projeto.
        /// </summary>
        public string? DocumentsEvaluationDescription { get; set; }

        public virtual Project? Project { get; set; }
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
        /// Pontuação Total Final.
        /// </summary>
        public double FinalScore { get; set; }

        /// <summary>
        /// Titulação do Orientador.
        /// Doutor (2); Mestre (1).
        /// </summary>
        private EQualification? _qualification;
        public EQualification? Qualification
        {
            get => _qualification;
            set
            {
                EntityExceptionValidation.When(value is null,
                    ExceptionMessageFactory.Required(nameof(Qualification)));
                _qualification = value;
            }
        }

        /// <summary>
        /// Foco e clareza quanto aos objetivos da proposta de projeto a ser desenvolvido pelo aluno.
        /// Excelente (4); Bom (3); Regular (2); Fraco (1).
        /// </summary>
        private EScore? _projectProposalObjectives;
        public EScore? ProjectProposalObjectives
        {
            get => _projectProposalObjectives;
            set
            {
                EntityExceptionValidation.When(value is null,
                    ExceptionMessageFactory.Required(nameof(ProjectProposalObjectives)));
                _projectProposalObjectives = value;
            }
        }

        /// <summary>
        /// Coerência entre a produção acadêmico-científica do orientador e a proposta de projeto.
        /// Excelente (4); Bom (3); Regular (2); Fraco (1).
        /// </summary>
        private EScore? _academicScientificProductionCoherence;
        public EScore? AcademicScientificProductionCoherence
        {
            get => _academicScientificProductionCoherence;
            set
            {
                EntityExceptionValidation.When(value is null,
                    ExceptionMessageFactory.Required(nameof(AcademicScientificProductionCoherence)));
                _academicScientificProductionCoherence = value;
            }
        }

        /// <summary>
        /// Adequação da metodologia da proposta aos objetivos e ao cronograma de execução.
        /// Excelente (4); Bom (3); Regular (2); Fraco (1).
        /// </summary>
        private EScore? _proposalMethodologyAdaptation;
        public EScore? ProposalMethodologyAdaptation
        {
            get => _proposalMethodologyAdaptation;
            set
            {
                EntityExceptionValidation.When(value is null,
                    ExceptionMessageFactory.Required(nameof(ProposalMethodologyAdaptation)));
                _proposalMethodologyAdaptation = value;
            }
        }

        /// <summary>
        /// Contribuição efetiva da proposta de projeto para formação em pesquisa do aluno.
        /// Excelente (4); Bom (3); Regular (2); Fraco (1).
        /// </summary>
        private EScore? _effectiveContributionToResearch;
        public EScore? EffectiveContributionToResearch
        {
            get => _effectiveContributionToResearch;
            set
            {
                EntityExceptionValidation.When(value is null,
                    ExceptionMessageFactory.Required(nameof(EffectiveContributionToResearch)));
                _effectiveContributionToResearch = value;
            }
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
            Qualification = qualification;
            ProjectProposalObjectives = projectProposalObjectives;
            AcademicScientificProductionCoherence = academicScientificProductionCoherence;
            ProposalMethodologyAdaptation = proposalMethodologyAdaptation;
            EffectiveContributionToResearch = effectiveContributionToResearch;
            APIndex = apIndex;

            // Define a descrição da avaliação da submissão.
            SubmissionEvaluationDescription = submissionEvaluationStatus == EProjectStatus.Accepted
                ? EProjectStatus.Accepted.GetDescription()
                : submissionEvaluationDescription;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Calcula a pontuação final do projeto considerando todos os critérios.
        /// </summary>
        public void CalculateFinalScore()
        {
            FinalScore = (double)Qualification!
                + (double)ProjectProposalObjectives!
                + (double)AcademicScientificProductionCoherence!
                + (double)ProposalMethodologyAdaptation!
                + (double)EffectiveContributionToResearch!
                + APIndex;
        }
        #endregion
    }
}