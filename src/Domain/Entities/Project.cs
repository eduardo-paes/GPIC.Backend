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

        /// <summary>
        /// O professor é bolsista de Produtividade?
        /// </summary>
        public bool IsProductivityFellow { get; set; }

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
        /// <summary>
        /// Periódicos indexados nas bases do tipo 1 ou constantes na base QUALIS do estrato superior (A1, A2 e B1) (1).
        /// </summary>
        private int? _workType1;
        public int? WorkType1
        {
            get => _workType1;
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _workType1 = value;
            }
        }

        /// <summary>
        /// Periódicos indexados nas bases do tipo 2 ou constantes na base QUALIS do estrato inferior (B2, B3, B4, B5) (2).
        /// </summary>
        private int? _workType2;
        public int? WorkType2
        {
            get => _workType2;
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _workType2 = value;
            }
        }

        /// <summary>
        /// Anais de Congressos indexados (3a).
        /// </summary>
        private int? _indexedConferenceProceedings;
        public int? IndexedConferenceProceedings
        {
            get => _indexedConferenceProceedings;
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _indexedConferenceProceedings = value;
            }
        }

        /// <summary>
        /// Anais de Congressos não indexados (3b).
        /// </summary>
        private int? _notIndexedConferenceProceedings;
        public int? NotIndexedConferenceProceedings
        {
            get => _notIndexedConferenceProceedings;
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _notIndexedConferenceProceedings = value;
            }
        }

        /// <summary>
        /// Livros - Completos
        /// </summary>
        private int? _completedBook;
        public int? CompletedBook
        {
            get => _completedBook;
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _completedBook = value;
            }
        }

        /// <summary>
        /// Livros - Organizados
        /// </summary>
        private int? _organizedBook;
        public int? OrganizedBook
        {
            get => _organizedBook;
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _organizedBook = value;
            }
        }

        /// <summary>
        /// Livros - Capítulos
        /// </summary>
        private int? _bookChapters;
        public int? BookChapters
        {
            get => _bookChapters;
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _bookChapters = value;
            }
        }

        /// <summary>
        /// Livros - Tradução
        /// </summary>
        private int? _bookTranslations;
        public int? BookTranslations
        {
            get => _bookTranslations;
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _bookTranslations = value;
            }
        }

        /// <summary>
        /// Participação em comissão editorial de editoras e instituições acadêmicas.
        /// </summary>
        private int? _participationEditorialCommittees;
        public int? ParticipationEditorialCommittees
        {
            get => _participationEditorialCommittees;
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _participationEditorialCommittees = value;
            }
        }
        #endregion

        #region Produção Artístca e Cultural - Produção Apresentada
        /// <summary>
        /// Autoria ou coautoria de CD ou DVD publicado como compositor ou intérprete principal (solo, duo ou regência) em todas as faixas.
        /// </summary>
        private int? _fullComposerSoloOrchestraAllTracks;
        public int? FullComposerSoloOrchestraAllTracks
        {
            get => _fullComposerSoloOrchestraAllTracks;
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _fullComposerSoloOrchestraAllTracks = value;
            }
        }

        /// <summary>
        /// Autoria ou coautoria de CD ou DVD publicado como compositor ou intérprete principal (solo, duo ou regência) em coletânea (sem participação em todas as faixas).
        /// </summary>
        private int? _fullComposerSoloOrchestraCompilation;
        public int? FullComposerSoloOrchestraCompilation
        {
            get => _fullComposerSoloOrchestraCompilation;
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _fullComposerSoloOrchestraCompilation = value;
            }
        }

        /// <summary>
        /// Participação em CD ou DVD como intérprete em grupo de câmara ou orquestra.
        /// </summary>
        private int? _chamberOrchestraInterpretation;
        public int? ChamberOrchestraInterpretation
        {
            get => _chamberOrchestraInterpretation;
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _chamberOrchestraInterpretation = value;
            }
        }

        /// <summary>
        /// Apresentações individuais e coletivas no campo das artes.
        /// </summary>
        private int? _individualAndCollectiveArtPerformances;
        public int? IndividualAndCollectiveArtPerformances
        {
            get => _individualAndCollectiveArtPerformances;
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _individualAndCollectiveArtPerformances = value;
            }
        }

        /// <summary>
        /// Curadoria de coleções ou exposições científicas, culturais e artísticas.
        /// </summary>
        private int? _scientificCulturalArtisticCollectionsCuratorship;
        public int? ScientificCulturalArtisticCollectionsCuratorship
        {
            get => _scientificCulturalArtisticCollectionsCuratorship;
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _scientificCulturalArtisticCollectionsCuratorship = value;
            }
        }

        #endregion

        #region Produção Técnica - Produtos Registrados
        /// <summary>
        /// Carta patente com titularidade do CEFET/RJ.
        /// </summary>
        private int? _patentLetter;
        public int? PatentLetter
        {
            get => _patentLetter;
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _patentLetter = value;
            }
        }

        /// <summary>
        /// Depósito de patente com titularidade do CEFET/RJ.
        /// </summary>
        private int? _patentDeposit;
        public int? PatentDeposit
        {
            get => _patentDeposit;
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _patentDeposit = value;
            }
        }

        /// <summary>
        /// Registro de Software.
        /// </summary>
        private int? _softwareRegistration;
        public int? SoftwareRegistration
        {
            get => _softwareRegistration;
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _softwareRegistration = value;
            }
        }
        #endregion

        #region Critérios de Avaliação
        /// <summary>
        /// Pontuação Total (Índice AP).
        /// </summary>
        private int? _apIndex;
        public int? APIndex
        {
            get => _apIndex;
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _apIndex = value;
            }
        }

        /// <summary>
        /// Titulação do Orientador.
        /// Doutor (2); Mestre (1).
        /// </summary>
        public EQualification? Qualification { get; set; }

        /// <summary>
        /// Foco e clareza quanto aos objetivos da proposta de projeto a ser desenvolvido pelo aluno.
        /// Excelente (4); Bom (3); Regular (2); Fraco (1).
        /// </summary>
        public EScore? ProjectProposalObjectives { get; set; }

        /// <summary>
        /// Coerência entre a produção acadêmico-científica do orientador e a proposta de projeto.
        /// Excelente (4); Bom (3); Regular (2); Fraco (1).
        /// </summary>
        public EScore? AcademicScientificProductionCoherence { get; set; }

        /// <summary>
        /// Adequação da metodologia da proposta aos objetivos e ao cronograma de execução.
        /// Excelente (4); Bom (3); Regular (2); Fraco (1).
        /// </summary>
        public EScore? ProposalMethodologyAdaptation { get; set; }

        /// <summary>
        /// Contribuição efetiva da proposta de projeto para formação em pesquisa do aluno.
        /// Excelente (4); Bom (3); Regular (2); Fraco (1).
        /// </summary>
        public EScore? EffectiveContributionToResearch { get; set; }

        #endregion

        #region Resultados da Avaliação
        /// <summary>
        /// Status do projeto.
        /// </summary>
        public EProjectStatus? Status { get; set; }

        /// <summary>
        /// Descrição do status.
        /// </summary>
        public string? StatusDescription { get; set; }

        /// <summary>
        /// Observação do avaliador após processo de análise.
        /// </summary>
        public string? EvaluatorObservation { get; set; }

        /// <summary>
        /// Descrição da solicitação de recurso do orientador.
        /// </summary>
        public string? AppealDescription { get; set; }

        /// <summary>
        /// Observação do avaliador após processo de análise do recurso.
        /// </summary>
        public string? AppealEvaluatorObservation { get; set; }
        #endregion

        #region Relacionamentos
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
            set
            {
                {
                    EntityExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required(nameof(value)));
                    _professorId = value;
                }
            }
        }

        private Guid? _studentId;
        public Guid? StudentId
        {
            get => _studentId;
            set
            {
                {
                    EntityExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required(nameof(value)));
                    _studentId = value;
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
        /// Data de submissão do projeto na plataforma.
        /// </summary>
        public DateTime SubmitionDate { get; set; }

        /// <summary>
        /// Data de ressubmissão do projeto na plataforma.
        /// </summary>
        public DateTime RessubmitionDate { get; set; }

        /// <summary>
        /// Data de cancelamento do projeto.
        /// </summary>
        public DateTime CancelationDate { get; set; }
        #endregion
        #endregion

        #region Constructors
        public Project(Guid? programTypeId, Guid? professorId, Guid? studentId, Guid? subAreaId, bool isScholarshipCandidate,
            string title, string keyWord1, string keyWord2, string keyWord3, string objective, string methodology, string expectedResults,
            string activitiesExecutionSchedule)
        {
            ProgramTypeId = programTypeId;
            ProfessorId = professorId;
            StudentId = studentId;
            SubAreaId = subAreaId;
            IsScholarshipCandidate = isScholarshipCandidate;
            Title = title;
            KeyWord1 = keyWord1;
            KeyWord2 = keyWord2;
            KeyWord3 = keyWord3;
            Objective = objective;
            Methodology = methodology;
            ExpectedResults = expectedResults;
            ActivitiesExecutionSchedule = activitiesExecutionSchedule;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        public Project() { }
        #endregion
    }
}