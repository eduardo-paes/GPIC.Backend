using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Enums;
using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    public class ProjectReport : Entity
    {
        /// <summary>
        /// URL do relatório.
        /// </summary>
        public string? ReportUrl { get; set; }

        private EReportType? _reportType;
        /// <summary>
        /// Tipo do relatório.
        /// </summary>
        public EReportType? ReportType
        {
            get { return _reportType; }
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(ReportType)));
                EntityExceptionValidation.When(!Enum.TryParse<EReportType>(value.ToString(), out var _),
                    ExceptionMessageFactory.Invalid(nameof(ReportType)));
                _reportType = value;
            }
        }

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

        public ProjectReport(EReportType? reportType, Guid? projectId)
        {
            ReportType = reportType;
            SendDate = DateTime.UtcNow;
            ProjectId = projectId;
        }
    }
}