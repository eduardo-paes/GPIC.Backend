using Domain.Entities.Enums;
using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    public class Project : Entity
    {
        #region Properties
        private string? _title;
        /// <summary>
        /// Título do Projeto de Iniciação Científica
        /// </summary>
        public string? Title
        {
            get => _title;
            private set
            {
                DomainExceptionValidation.When(string.IsNullOrWhiteSpace(value),
                    ExceptionMessageFactory.Invalid("Título"));
                _title = value;
            }
        }

        private string? _keyWord1;
        public string? KeyWord1
        {
            get => _keyWord1;
            private set
            {
                DomainExceptionValidation.When(string.IsNullOrWhiteSpace(value),
                    ExceptionMessageFactory.Invalid("Palavra-chave 1"));
                _keyWord1 = value;
            }
        }

        private string? _keyWord2;
        public string? KeyWord2
        {
            get => _keyWord2;
            private set
            {
                DomainExceptionValidation.When(string.IsNullOrWhiteSpace(value),
                    ExceptionMessageFactory.Invalid("Palavra-chave 2"));
                _keyWord2 = value;
            }
        }

        private string? _keyWord3;
        public string? KeyWord3
        {
            get => _keyWord3;
            private set
            {
                DomainExceptionValidation.When(string.IsNullOrWhiteSpace(value),
                    ExceptionMessageFactory.Invalid("Palavra-chave 3"));
                _keyWord3 = value;
            }
        }

        /// <summary>
        /// É candidato a Bolsa?
        /// </summary>
        public bool IsScholarshipCandidate { get; private set; }

        private string? _objective;
        /// <summary>
        /// Proposta do Projeto IC: Objetivo
        /// </summary>
        public string? Objective
        {
            get => _objective;
            private set
            {
                DomainExceptionValidation.When(string.IsNullOrWhiteSpace(value),
                    ExceptionMessageFactory.Invalid("Objetivo"));
                DomainExceptionValidation.When(value.Length > 1500,
                    ExceptionMessageFactory.MaxLength("Objetivo", 1500));
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
            private set
            {
                DomainExceptionValidation.When(string.IsNullOrWhiteSpace(value),
                    ExceptionMessageFactory.Invalid("Metodologia"));
                DomainExceptionValidation.When(value.Length > 1500,
                    ExceptionMessageFactory.MaxLength("Metodologia", 1500));
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
            private set
            {
                DomainExceptionValidation.When(string.IsNullOrWhiteSpace(value),
                    ExceptionMessageFactory.Invalid("Resultados Esperados"));
                DomainExceptionValidation.When(value.Length > 1500,
                    ExceptionMessageFactory.MaxLength("Resultados Esperados", 1500));
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
            private set
            {
                DomainExceptionValidation.When(string.IsNullOrWhiteSpace(value),
                    ExceptionMessageFactory.Invalid("Cronograma de Execução das Atividades"));
                _activitiesExecutionSchedule = value;
            }
        }

        private EProgramType? _programTypeId;
        public EProgramType? ProgramTypeId
        {
            get => _programTypeId;
            private set
            {
                {
                    DomainExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required("tipo de programa"));
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
                    DomainExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required("id do professor"));
                    _professorId = value;
                }
            }
        }

        private Guid? _studentId;
        public Guid? StudentId
        {
            get => _studentId;
            private set
            {
                {
                    DomainExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required("id do estudante"));
                    _studentId = value;
                }
            }
        }

        private Guid? _subAreaId;
        public Guid? SubAreaId
        {
            get => _subAreaId;
            private set
            {
                {
                    DomainExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required("id da sub área"));
                    _subAreaId = value;
                }
            }
        }
        #endregion

        #region Constructors
        public Project(EProgramType? programTypeId, Guid? professorId, Guid? studentId, Guid? subAreaId, bool isScholarshipCandidate,
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

        #region Updaters
        public void UpdateProgramTypeId(EProgramType? programTypeId) => ProgramTypeId = programTypeId;
        public void UpdateProfessorId(Guid? professorId) => ProfessorId = professorId;
        public void UpdateStudentId(Guid? studentId) => StudentId = studentId;
        public void UpdateSubAreaId(Guid? subAreaId) => SubAreaId = subAreaId;
        public void UpdateIsScholarshipCandidate(bool isScholarshipCandidate) => IsScholarshipCandidate = isScholarshipCandidate;
        public void UpdateTitle(string? title) => Title = title;
        public void UpdateKeyWord1(string? keyWord1) => KeyWord1 = keyWord1;
        public void UpdateKeyWord2(string? keyWord2) => KeyWord2 = keyWord2;
        public void UpdateKeyWord3(string? keyWord3) => KeyWord3 = keyWord3;
        public void UpdateObjective(string? objective) => Objective = objective;
        public void UpdateMethodology(string? methodology) => Methodology = methodology;
        public void UpdateExpectedResults(string? expectedResults) => ExpectedResults = expectedResults;
        public void UpdateActivitiesExecutionSchedule(string? activitiesExecutionSchedule) => ActivitiesExecutionSchedule = activitiesExecutionSchedule;
        #endregion
    }
}