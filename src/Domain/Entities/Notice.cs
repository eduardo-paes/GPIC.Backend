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
        #region Registration Dates
        private DateTime? _registrationStartDate;
        /// <summary>
        /// Data de início das inscrições dos projetos.
        /// </summary>
        public DateTime? RegistrationStartDate
        {
            get => _registrationStartDate;
            set
            {
                EntityExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Invalid("Data de início das inscrições dos projetos"));
                _registrationStartDate = value;
            }
        }

        private DateTime? _registrationEndDate;
        /// <summary>
        /// Data de término das inscrições dos projetos.
        /// </summary>
        public DateTime? RegistrationEndDate
        {
            get => _registrationEndDate;
            set
            {
                EntityExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Invalid("Data de término das inscrições dos projetos"));
                _registrationEndDate = value;
            }
        }
        #endregion

        #region Evaluation Dates
        /// <summary>
        /// Data de início das avaliações dos projetos.
        /// </summary>
        private DateTime? _evaluationStartDate;
        public DateTime? EvaluationStartDate
        {
            get => _evaluationStartDate;
            set
            {
                EntityExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Invalid("Data de início das avaliações dos projetos"));
                _evaluationStartDate = value;
            }
        }

        /// <summary>
        /// Data de término das avaliações dos projetos.
        /// </summary>
        private DateTime? _evaluationEndDate;
        public DateTime? EvaluationEndDate
        {
            get => _evaluationEndDate;
            set
            {
                EntityExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Invalid("Data de término das avaliações dos projetos"));
                _evaluationEndDate = value;
            }
        }
        #endregion

        #region Appeal Dates
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
                    ExceptionMessageFactory.Invalid("Data de início do período de recurso"));
                _appealStartDate = value;
            }
        }

        private DateTime? _appealEndDate;
        /// <summary>
        /// Data de término do período de recurso.
        /// </summary>
        public DateTime? AppealEndDate
        {
            get => _appealEndDate;
            set
            {
                EntityExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Invalid("Data de término do período de recurso"));
                _appealEndDate = value;
            }
        }
        #endregion

        #region Sending Documentation Dates
        /// <summary>
        /// Data de início para entrega de documentação dos bolsistas.
        /// </summary>
        private DateTime? _sendingDocsStartDate;
        public DateTime? SendingDocsStartDate
        {
            get => _sendingDocsStartDate;
            set
            {
                EntityExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Invalid("Data de início para entrega de documentação dos bolsistas"));
                _sendingDocsStartDate = value;
            }
        }

        /// <summary>
        /// Data de término para entrega de documentação dos bolsistas.
        /// </summary>
        private DateTime? _sendingDocsEndDate;
        public DateTime? SendingDocsEndDate
        {
            get => _sendingDocsEndDate;
            set
            {
                EntityExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Invalid("Data de término para entrega de documentação dos bolsistas"));
                _sendingDocsEndDate = value;
            }
        }
        #endregion

        #region Report Sending Dates
        /// <summary>
        /// Prazo de entrega do relatório parcial (Data fim).
        /// </summary>
        private DateTime? _partialReportDeadline;
        public DateTime? PartialReportDeadline
        {
            get => _partialReportDeadline;
            set
            {
                EntityExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Invalid("Prazo de entrega do relatório parcial"));
                _partialReportDeadline = value;
            }
        }

        /// <summary>
        /// Prazo de entrega do relatório final (Data fim).
        /// </summary>
        private DateTime? _finalReportDeadline;
        public DateTime? FinalReportDeadline
        {
            get => _finalReportDeadline;
            set
            {
                EntityExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Invalid("Prazo de entrega do relatório final"));
                _finalReportDeadline = value;
            }
        }
        #endregion

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
                    ExceptionMessageFactory.Required("Anos de suspensão do orientador"));
                EntityExceptionValidation.When(value < 0,
                    ExceptionMessageFactory.Invalid("Anos de suspensão do orientador"));
                _suspensionYears = value;
            }
        }

        /// <summary>
        /// URL do edital
        /// </summary>
        public string? DocUrl { get; set; }

        /// <summary>
        /// Descrição do edital
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Data de criação do edital
        /// </summary>
        public DateTime? CreatedAt { get; protected set; }
        #endregion

        #region Constructors
        public Notice(DateTime? registrationStartDate,
            DateTime? registrationEndDate,
            DateTime? evaluationStartDate,
            DateTime? evaluationEndDate,
            DateTime? appealStartDate,
            DateTime? appealFinalDate,
            DateTime? sendingDocsStartDate,
            DateTime? sendingDocsEndDate,
            DateTime? partialReportDeadline,
            DateTime? finalReportDeadline,
            string? description,
            int? suspensionYears)
        {
            RegistrationStartDate = registrationStartDate;
            RegistrationEndDate = registrationEndDate;
            EvaluationStartDate = evaluationStartDate;
            EvaluationEndDate = evaluationEndDate;
            AppealStartDate = appealStartDate;
            AppealEndDate = appealFinalDate;
            SendingDocsStartDate = sendingDocsStartDate;
            SendingDocsEndDate = sendingDocsEndDate;
            SuspensionYears = suspensionYears;
            PartialReportDeadline = partialReportDeadline;
            FinalReportDeadline = finalReportDeadline;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }

        public Notice(
            Guid? id,
            DateTime? registrationStartDate,
            DateTime? registrationEndDate,
            DateTime? evaluationStartDate,
            DateTime? evaluationEndDate,
            DateTime? appealStartDate,
            DateTime? appealFinalDate,
            DateTime? sendingDocsStartDate,
            DateTime? sendingDocsEndDate,
            DateTime? partialReportDeadline,
            DateTime? finalReportDeadline,
            string? description,
            int? suspensionYears)
        {
            Id = id;
            RegistrationStartDate = registrationStartDate;
            RegistrationEndDate = registrationEndDate;
            EvaluationStartDate = evaluationStartDate;
            EvaluationEndDate = evaluationEndDate;
            AppealStartDate = appealStartDate;
            AppealEndDate = appealFinalDate;
            SendingDocsStartDate = sendingDocsStartDate;
            SendingDocsEndDate = sendingDocsEndDate;
            SuspensionYears = suspensionYears;
            PartialReportDeadline = partialReportDeadline;
            FinalReportDeadline = finalReportDeadline;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        protected Notice() { }
        #endregion
    }
}