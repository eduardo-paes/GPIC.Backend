
using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    /// <summary>
    /// Professor
    /// </summary>
    public class Professor : Entity
    {
        #region Properties
        /// <summary>
        /// Matrícula SIAPE
        /// </summary>
        private string? _siapeEnrollment;
        public string? SIAPEEnrollment
        {
            get => _siapeEnrollment;
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("Matrícula SIAPE"));
                EntityExceptionValidation.When(value?.Length != 7,
                    ExceptionMessageFactory.WithLength("Matrícula SIAPE", 7));
                _siapeEnrollment = value;
            }
        }

        /// <summary>
        /// Identificar Lattes
        /// </summary>
        private long _identifyLattes;
        public long IdentifyLattes
        {
            get => _identifyLattes;
            set
            {
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid("Identificador Lattes"));
                _identifyLattes = value;
            }
        }

        /// <summary>
        /// ID do usuário
        /// </summary>
        private Guid? _userId;
        public Guid? UserId
        {
            get { return _userId; }
            set
            {
                {
                    EntityExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required("id do usuário"));
                    _userId = value;
                }
            }
        }

        /// <summary>
        /// Data de início da suspensão do professor
        /// </summary>
        private DateTime? _suspensionEndDate;
        public DateTime? SuspensionEndDate
        {
            get => _suspensionEndDate;
            set
            {
                _suspensionEndDate = value;
            }
        }

        public virtual User? User { get; }
        #endregion

        #region Constructors
        public Professor(string? siapeEnrollment, long identifyLattes)
        {
            SIAPEEnrollment = siapeEnrollment;
            IdentifyLattes = identifyLattes;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        protected Professor() { }
        #endregion
    }
}