using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    public class ProjectFinalReport : Entity
    {
        /// <summary>
        /// URL do relatório.
        /// </summary>
        public string? ReportUrl { get; set; }

        private DateTime? _sendDate;
        /// <summary>
        /// Data de envio do relatório.
        /// </summary>
        public DateTime? SendDate
        {
            get => _sendDate;
            set
            {
                EntityExceptionValidation.When(value is null,
                    ExceptionMessageFactory.Required(nameof(SendDate)));
                _sendDate = value;
            }
        }

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

        public ProjectFinalReport(Guid? projectId)
        {
            SendDate = DateTime.UtcNow;
            ProjectId = projectId;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        protected ProjectFinalReport() { }
    }
}