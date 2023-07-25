using Domain.Entities.Enums;
using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    /// <summary>
    /// Projeto de Iniciação Científica
    /// </summary>
    public class Project : Entity
    {
        #region Properties
        #region Informações do Projeto
        private string? _title;
        /// <summary>
        /// Título do Projeto de Iniciação Científica
        /// </summary>
        public string? Title
        {
            get => _title;
            set
            {
                EntityExceptionValidation.When(string.IsNullOrWhiteSpace(value),
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _title = value;
            }
        }

        private string? _keyWord1;
        public string? KeyWord1
        {
            get => _keyWord1;
            set
            {
                EntityExceptionValidation.When(string.IsNullOrWhiteSpace(value),
                    ExceptionMessageFactory.Invalid(nameof(value)));
                EntityExceptionValidation.When(value!.Length > 100,
                    ExceptionMessageFactory.MaxLength(nameof(value), 100));
                _keyWord1 = value;
            }
        }

        private string? _keyWord2;
        public string? KeyWord2
        {
            get => _keyWord2;
            set
            {
                EntityExceptionValidation.When(string.IsNullOrWhiteSpace(value),
                    ExceptionMessageFactory.Invalid(nameof(value)));
                EntityExceptionValidation.When(value!.Length > 100,
                    ExceptionMessageFactory.MaxLength(nameof(value), 100));
                _keyWord2 = value;
            }
        }

        private string? _keyWord3;
        public string? KeyWord3
        {
            get => _keyWord3;
            set
            {
                EntityExceptionValidation.When(string.IsNullOrWhiteSpace(value),
                    ExceptionMessageFactory.Invalid(nameof(value)));
                EntityExceptionValidation.When(value!.Length > 100,
                    ExceptionMessageFactory.MaxLength(nameof(value), 100));
                _keyWord3 = value;
            }
        }

        /// <summary>
        /// O aluno é candidato à Bolsa?
        /// </summary>
        public bool IsScholarshipCandidate { get; set; }

        private string? _objective;
        /// <summary>
        /// Proposta do Projeto IC: Objetivo
        /// </summary>
        public string? Objective
        {
            get => _objective;
            set
            {
                EntityExceptionValidation.When(string.IsNullOrWhiteSpace(value),
                    ExceptionMessageFactory.Invalid(nameof(value)));
                EntityExceptionValidation.When(value?.Length > 1500,
                    ExceptionMessageFactory.MaxLength(nameof(value), 1500));
                _objective = value;
            }
        }

        private string? _methodology;
        /// <summary>
        /// Proposta do Projeto IC: Metodologia
        /// </summary>
        public string? Methodology
        {
            get => _methodology;
            set
            {
                EntityExceptionValidation.When(string.IsNullOrWhiteSpace(value),
                    ExceptionMessageFactory.Invalid(nameof(value)));
                EntityExceptionValidation.When(value?.Length > 1500,
                    ExceptionMessageFactory.MaxLength(nameof(value), 1500));
                _methodology = value;
            }
        }

        private string? _expectedResults;
        /// <summary>
        /// Proposta do Projeto IC: Resultados Esperados
        /// </summary>
        public string? ExpectedResults
        {
            get => _expectedResults;
            set
            {
                EntityExceptionValidation.When(string.IsNullOrWhiteSpace(value),
                    ExceptionMessageFactory.Invalid(nameof(value)));
                EntityExceptionValidation.When(value?.Length > 1500,
                    ExceptionMessageFactory.MaxLength(nameof(value), 1500));
                _expectedResults = value;
            }
        }

        private string? _activitiesExecutionSchedule;
        /// <summary>
        /// Cronograma de Execução das Atividades
        /// </summary>
        public string? ActivitiesExecutionSchedule
        {
            get => _activitiesExecutionSchedule;
            set
            {
                EntityExceptionValidation.When(string.IsNullOrWhiteSpace(value),
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _activitiesExecutionSchedule = value;
            }
        }
        #endregion

        #region Relacionamentos
        public Guid? StudentId;

        private Guid? _programTypeId;
        public Guid? ProgramTypeId
        {
            get => _programTypeId;
            set
            {
                {
                    EntityExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required(nameof(value)));
                    _programTypeId = value;
                }
            }
        }

        private Guid? _professorId;
        public Guid? ProfessorId
        {
            get => _professorId;
            private set
            {
                {
                    EntityExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required(nameof(value)));
                    _professorId = value;
                }
            }
        }

        private Guid? _subAreaId;
        public Guid? SubAreaId
        {
            get => _subAreaId;
            set
            {
                {
                    EntityExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required(nameof(value)));
                    _subAreaId = value;
                }
            }
        }

        private Guid? _noticeId;
        public Guid? NoticeId
        {
            get => _noticeId;
            private set
            {
                {
                    EntityExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required(nameof(value)));
                    _noticeId = value;
                }
            }
        }

        public virtual ProgramType? ProgramType { get; }
        public virtual Professor? Professor { get; }
        public virtual Student? Student { get; }
        public virtual SubArea? SubArea { get; }
        public virtual Notice? Notice { get; }
        public virtual IList<ProjectActivity>? ProjectActivities { get; }
        #endregion

        #region Informações de Controle
        /// <summary>
        /// Status do projeto.
        /// </summary>
        public EProjectStatus? Status { get; set; }

        /// <summary>
        /// Descrição do status.
        /// </summary>
        public string? StatusDescription { get; set; }

        /// <summary>
        /// Descrição da solicitação de recurso do orientador.
        /// </summary>
        public string? AppealObservation { get; set; }

        /// <summary>
        /// Data de submissão do projeto na plataforma.
        /// </summary>
        private DateTime? _submissionDate;
        public DateTime? SubmissionDate
        {
            get { return _submissionDate; }
            set { _submissionDate = value.HasValue ? value.Value.ToUniversalTime() : null; }
        }

        /// <summary>
        /// Data de ressubmissão do projeto na plataforma.
        /// </summary>
        private DateTime? _appealDate;
        public DateTime? AppealDate
        {
            get { return _appealDate; }
            set { _appealDate = value.HasValue ? value.Value.ToUniversalTime() : null; }
        }

        /// <summary>
        /// Data de cancelamento do projeto.
        /// </summary>
        private DateTime? _cancellationDate;
        public DateTime? CancellationDate
        {
            get { return _cancellationDate; }
            set { _cancellationDate = value.HasValue ? value.Value.ToUniversalTime() : null; }
        }

        /// <summary>
        /// Razão de cancelamento do projeto, preenchido pelo professor.
        /// </summary>
        public string? CancellationReason { get; set; }
        #endregion
        #endregion

        #region Constructors
        public Project(string? title, string? keyWord1, string? keyWord2, string? keyWord3, bool isScholarshipCandidate,
                       string? objective, string? methodology, string? expectedResults, string? activitiesExecutionSchedule,
                       Guid? studentId, Guid? programTypeId, Guid? professorId, Guid? subAreaId, Guid? noticeId,
                       EProjectStatus? status, string? statusDescription, string? appealDescription,
                       DateTime? submitionDate, DateTime? ressubmissionDate, DateTime? cancellationDate,
                       string? cancellationReason)
        {
            // Inicializar as propriedades
            Title = title;
            KeyWord1 = keyWord1;
            KeyWord2 = keyWord2;
            KeyWord3 = keyWord3;
            IsScholarshipCandidate = isScholarshipCandidate;
            Objective = objective;
            Methodology = methodology;
            ExpectedResults = expectedResults;
            ActivitiesExecutionSchedule = activitiesExecutionSchedule;
            StudentId = studentId;
            ProgramTypeId = programTypeId;
            ProfessorId = professorId;
            SubAreaId = subAreaId;
            NoticeId = noticeId;
            Status = status;
            StatusDescription = statusDescription;
            AppealObservation = appealDescription;
            SubmissionDate = submitionDate;
            AppealDate = ressubmissionDate;
            CancellationDate = cancellationDate;
            CancellationReason = cancellationReason;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        protected Project() { }
        #endregion
    }
}