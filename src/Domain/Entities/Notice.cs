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
        public DateTime? StartDate
        {
            get { return _startDate; }
            set
            {
                DomainExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Invalid("Data Inicial"));
                _startDate = value.HasValue ? value.Value.ToUniversalTime() : null;
            }
        }

        private DateTime? _finalDate;
        public DateTime? FinalDate
        {
            get { return _finalDate; }
            set
            {
                DomainExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Invalid("Data Final"));
                _finalDate = value.HasValue ? value.Value.ToUniversalTime() : null;
            }
        }

        public string? Description { get; set; }

        private string? _docUrl;
        public string? DocUrl
        {
            get { return _docUrl; }
            set
            {
                DomainExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Invalid("URL do Edital"));
                _docUrl = value;
            }
        }
        #endregion

        #region Constructors
        public Notice(DateTime? startDate, DateTime? finalDate, string? description, string? docUrl)
        {
            StartDate = startDate;
            FinalDate = finalDate;
            Description = description;
            DocUrl = docUrl;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        public Notice() { }
        #endregion
    }
}