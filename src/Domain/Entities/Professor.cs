
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
                DomainExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("Matrícula SIAPE"));
                DomainExceptionValidation.When(value.Length != 7,
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
                DomainExceptionValidation.When(value <= 0,
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
            private set
            {
                {
                    DomainExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required("id do usuário"));
                    _userId = value;
                }
            }
        }
        #endregion

        #region Constructors
        public Professor(string? siapeEnrollment, long identifyLattes, Guid? userId)
        {
            SIAPEEnrollment = siapeEnrollment;
            IdentifyLattes = identifyLattes;
            UserId = userId;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        public Professor() { }
        #endregion
    }
}