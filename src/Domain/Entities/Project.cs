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

        #region Produção Científica - Trabalhos Publicados
        private int? _workType1;
        /// <summary>
        /// Periódicos indexados nas bases do tipo 1 ou constantes na base QUALIS do estrato superior (A1, A2 e B1) (1).
        /// </summary>
        public int? WorkType1
        {
            get => _workType1;
            set
            {
                value ??= 0;
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _workType1 = value;
            }
        }

        private int? _workType2;
        /// <summary>
        /// Periódicos indexados nas bases do tipo 2 ou constantes na base QUALIS do estrato inferior (B2, B3, B4, B5) (2).
        /// </summary>
        public int? WorkType2
        {
            get => _workType2;
            set
            {
                value ??= 0;
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _workType2 = value;
            }
        }

        private int? _indexedConferenceProceedings;
        /// <summary>
        /// Anais de Congressos indexados (3a).
        /// </summary>
        public int? IndexedConferenceProceedings
        {
            get => _indexedConferenceProceedings;
            set
            {
                value ??= 0;
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _indexedConferenceProceedings = value;
            }
        }

        private int? _notIndexedConferenceProceedings;
        /// <summary>
        /// Anais de Congressos não indexados (3b).
        /// </summary>
        public int? NotIndexedConferenceProceedings
        {
            get => _notIndexedConferenceProceedings;
            set
            {
                value ??= 0;
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _notIndexedConferenceProceedings = value;
            }
        }

        private int? _completedBook;
        /// <summary>
        /// Livros - Completos
        /// </summary>
        public int? CompletedBook
        {
            get => _completedBook;
            set
            {
                value ??= 0;
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _completedBook = value;
            }
        }

        private int? _organizedBook;
        /// <summary>
        /// Livros - Organizados
        /// </summary>
        public int? OrganizedBook
        {
            get => _organizedBook;
            set
            {
                value ??= 0;
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _organizedBook = value;
            }
        }

        private int? _bookChapters;
        /// <summary>
        /// Livros - Capítulos
        /// </summary>
        public int? BookChapters
        {
            get => _bookChapters;
            set
            {
                value ??= 0;
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _bookChapters = value;
            }
        }

        private int? _bookTranslations;
        /// <summary>
        /// Livros - Tradução
        /// </summary>
        public int? BookTranslations
        {
            get => _bookTranslations;
            set
            {
                value ??= 0;
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _bookTranslations = value;
            }
        }

        private int? _participationEditorialCommittees;
        /// <summary>
        /// Participação em comissão editorial de editoras e instituições acadêmicas.
        /// </summary>
        public int? ParticipationEditorialCommittees
        {
            get => _participationEditorialCommittees;
            set
            {
                value ??= 0;
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _participationEditorialCommittees = value;
            }
        }
        #endregion

        #region Produção Artístca e Cultural - Produção Apresentada
        private int? _fullComposerSoloOrchestraAllTracks;
        /// <summary>
        /// Autoria ou coautoria de CD ou DVD publicado como compositor ou intérprete principal (solo, duo ou regência) em todas as faixas.
        /// </summary>
        public int? FullComposerSoloOrchestraAllTracks
        {
            get => _fullComposerSoloOrchestraAllTracks;
            set
            {
                value ??= 0;
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _fullComposerSoloOrchestraAllTracks = value;
            }
        }

        private int? _fullComposerSoloOrchestraCompilation;
        /// <summary>
        /// Autoria ou coautoria de CD ou DVD publicado como compositor ou intérprete principal (solo, duo ou regência) em coletânea (sem participação em todas as faixas).
        /// </summary>
        public int? FullComposerSoloOrchestraCompilation
        {
            get => _fullComposerSoloOrchestraCompilation;
            set
            {
                value ??= 0;
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _fullComposerSoloOrchestraCompilation = value;
            }
        }

        private int? _chamberOrchestraInterpretation;
        /// <summary>
        /// Participação em CD ou DVD como intérprete em grupo de câmara ou orquestra.
        /// </summary>
        public int? ChamberOrchestraInterpretation
        {
            get => _chamberOrchestraInterpretation;
            set
            {
                value ??= 0;
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _chamberOrchestraInterpretation = value;
            }
        }

        private int? _individualAndCollectiveArtPerformances;
        /// <summary>
        /// Apresentações individuais e coletivas no campo das artes.
        /// </summary>
        public int? IndividualAndCollectiveArtPerformances
        {
            get => _individualAndCollectiveArtPerformances;
            set
            {
                value ??= 0;
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _individualAndCollectiveArtPerformances = value;
            }
        }

        private int? _scientificCulturalArtisticCollectionsCuratorship;
        /// <summary>
        /// Curadoria de coleções ou exposições científicas, culturais e artísticas.
        /// </summary>
        public int? ScientificCulturalArtisticCollectionsCuratorship
        {
            get => _scientificCulturalArtisticCollectionsCuratorship;
            set
            {
                value ??= 0;
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _scientificCulturalArtisticCollectionsCuratorship = value;
            }
        }
        #endregion

        #region Produção Técnica - Produtos Registrados
        private int? _patentLetter;
        /// <summary>
        /// Carta patente com titularidade do CEFET/RJ.
        /// </summary>
        public int? PatentLetter
        {
            get => _patentLetter;
            set
            {
                value ??= 0;
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _patentLetter = value;
            }
        }

        private int? _patentDeposit;
        /// <summary>
        /// Depósito de patente com titularidade do CEFET/RJ.
        /// </summary>
        public int? PatentDeposit
        {
            get => _patentDeposit;
            set
            {
                value ??= 0;
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _patentDeposit = value;
            }
        }

        private int? _softwareRegistration;
        /// <summary>
        /// Registro de Software.
        /// </summary>
        public int? SoftwareRegistration
        {
            get => _softwareRegistration;
            set
            {
                value ??= 0;
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _softwareRegistration = value;
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
        public string? AppealDescription { get; set; }

        /// <summary>
        /// Data de submissão do projeto na plataforma.
        /// </summary>
        public DateTime? SubmitionDate { get; set; }

        /// <summary>
        /// Data de ressubmissão do projeto na plataforma.
        /// </summary>
        public DateTime? RessubmitionDate { get; set; }

        /// <summary>
        /// Data de cancelamento do projeto.
        /// </summary>
        public DateTime? CancellationDate { get; set; }

        /// <summary>
        /// Razão de cancelamento do projeto, preenchido pelo professor.
        /// </summary>
        public string? CancellationReason { get; set; }
        #endregion
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        public Project() { }
        #endregion
    }
}