using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    /// <summary>
    /// Edital de seleção de bolsistas
    /// </summary>
    public class Notice : Entity
    {
        #region Properties
        private DateTime? _startDate;
        /// <summary>
        /// Data de início do edital.
        /// </summary>
        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                EntityExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Invalid(nameof(StartDate)));
                _startDate = value.HasValue ? value.Value.ToUniversalTime() : null;
            }
        }

        private DateTime? _finalDate;
        /// <summary>
        /// Data de término do edital.
        /// </summary>
        public DateTime? FinalDate
        {
            get => _finalDate;
            set
            {
                EntityExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Invalid(nameof(FinalDate)));
                _finalDate = value.HasValue ? value.Value.ToUniversalTime() : null;
            }
        }

        private DateTime? _appealStartDate;
        /// <summary>
        /// Data de início do período de recurso.
        /// </summary>
        public DateTime? AppealStartDate
        {
            get => _appealStartDate;
            set
            {
                EntityExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Invalid(nameof(AppealStartDate)));
                _appealStartDate = value.HasValue ? value.Value.ToUniversalTime() : null;
            }
        }

        private DateTime? _appealFinalDate;
        /// <summary>
        /// Data de término do período de recurso.
        /// </summary>
        public DateTime? AppealFinalDate
        {
            get => _appealFinalDate;
            set
            {
                EntityExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Invalid(nameof(AppealFinalDate)));
                _appealFinalDate = value.HasValue ? value.Value.ToUniversalTime() : null;
            }
        }

        private int? _suspensionYears;
        /// <summary>
        /// Anos de suspensão do orientador em caso de não entrega do relatório final.
        /// </summary>
        public int? SuspensionYears
        {
            get => _suspensionYears;
            set
            {
                EntityExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Required(nameof(SuspensionYears)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(SuspensionYears)));
                _suspensionYears = value;
            }
        }

        private int? _sendingDocumentationDeadline;
        /// <summary>
        /// Prazo para envio da documentação em dias.
        /// </summary>
        public int? SendingDocumentationDeadline
        {
            get => _sendingDocumentationDeadline;
            set
            {
                EntityExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Required(nameof(SuspensionYears)));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid(nameof(SuspensionYears)));
                _sendingDocumentationDeadline = value;
            }
        }

        /// <summary>
        /// Descrição do edital
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// URL do edital
        /// </summary>
        public string? DocUrl { get; set; }
        #endregion

        #region Constructors
        public Notice(DateTime? startDate,
            DateTime? finalDate,
            DateTime? appealStartDate,
            DateTime? appealFinalDate,
            int? suspensionYears,
            int? sendingDocumentationDeadline)
        {
            StartDate = startDate;
            FinalDate = finalDate;
            AppealStartDate = appealStartDate;
            AppealFinalDate = appealFinalDate;
            SuspensionYears = suspensionYears;
            SendingDocumentationDeadline = sendingDocumentationDeadline;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        public Notice() { }
        #endregion
    }
}