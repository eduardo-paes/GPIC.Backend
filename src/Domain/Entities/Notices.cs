using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    public class Notices : Entity
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
                _startDate = value;
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
                _finalDate = value;
            }
        }

        private string? _description;
        public string? Description
        {
            get { return _description; }
            set { _description = value; }
        }

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
        public Notices(DateTime? startDate, DateTime? finalDate, string? description, string? docUrl)
        {
            StartDate = startDate;
            FinalDate = finalDate;
            Description = description;
            DocUrl = docUrl;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        public Notices() { }
        #endregion

        #region Updaters
        public void UpdateStartDate(DateTime? value) => StartDate = value;
        public void UpdateFinalDate(DateTime? value) => FinalDate = value;
        public void UpdateDescription(string? value) => Description = value;
        public void UpdateDocUrl(string? value) => DocUrl = value;
        #endregion
    }
}