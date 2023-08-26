using Domain.Entities.Enums;
using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    public class ProjectPartialReport : Entity
    {
        private int _currentDevelopmentStage;
        /// <summary>
        /// Estágio atual de desenvolvimento do Projeto de IC.
        /// </summary>
        /// <value>Valor entre 0 e 100.</value>
        public int CurrentDevelopmentStage
        {
            get { return _currentDevelopmentStage; }
            set
            {
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.MinValue(nameof(CurrentDevelopmentStage), 0));
                EntityExceptionValidation.When(value >= 100,
                    ExceptionMessageFactory.MinValue(nameof(CurrentDevelopmentStage), 100));
                _currentDevelopmentStage = value;
            }
        }

        private EScholarPerformance? _scholarPerformance;
        /// <summary>
        /// Desempenho do bolsista segundo o orientador.
        /// </summary>
        /// <value>Valores possíveis: "Ruim", "Regular", "Bom", "Muito Bom", "Excelente".</value>
        public EScholarPerformance? ScholarPerformance
        {
            get { return _scholarPerformance; }
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(ScholarPerformance)));
                _scholarPerformance = value;
            }
        }

        /// <summary>
        /// Informações adicionais sobre o projeto.
        /// </summary>
        public string? AdditionalInfo { get; set; }

        private Guid? _projectId;
        /// <summary>
        /// Id do projeto.
        /// </summary>
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

        private Guid? _userId;
        /// <summary>
        /// Id do usuário que fez o envio do relatório.
        /// </summary>
        public Guid? UserId
        {
            get => _userId;
            set
            {
                EntityExceptionValidation.When(value is null,
                    ExceptionMessageFactory.Required(nameof(UserId)));
                _userId = value;
            }
        }

        public virtual Project? Project { get; set; }
        public virtual User? User { get; set; }

        public ProjectPartialReport(Guid? projectId, int currentDevelopmentStage, EScholarPerformance? scholarPerformance, string? additionalInfo, Guid? userId)
        {
            ProjectId = projectId;
            CurrentDevelopmentStage = currentDevelopmentStage;
            ScholarPerformance = scholarPerformance;
            AdditionalInfo = additionalInfo;
            UserId = userId;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        protected ProjectPartialReport() { }
    }
}