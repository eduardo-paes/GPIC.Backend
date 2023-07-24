using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    public class ActivityType : Entity
    {
        #region Properties
        private string? _name;
        public string? Name
        {
            get { return _name; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required(nameof(Name)));
                EntityExceptionValidation.When(value?.Length < 3,
                    ExceptionMessageFactory.MinLength(nameof(Name), 3));
                EntityExceptionValidation.When(value?.Length > 300,
                    ExceptionMessageFactory.MaxLength(nameof(Name), 300));
                _name = value;
            }
        }

        private string? _unity;
        public string? Unity
        {
            get { return _unity; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required(nameof(Unity)));
                EntityExceptionValidation.When(value?.Length < 3,
                    ExceptionMessageFactory.MinLength(nameof(Unity), 3));
                EntityExceptionValidation.When(value?.Length > 300,
                    ExceptionMessageFactory.MaxLength(nameof(Unity), 300));
                _unity = value;
            }
        }

        private Guid? _noticeId;
        public Guid? NoticeId
        {
            get => _noticeId;
            private set
            {
                {
                    EntityExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required("Id do Edital"));
                    _noticeId = value;
                }
            }
        }

        public virtual Notice? Notice { get; }
        public virtual IList<Activity>? Activities { get; }
        #endregion

        #region Constructors
        public ActivityType(string? name, string? unity, Guid? noticeId)
        {
            Name = name;
            Unity = unity;
            NoticeId = noticeId;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        protected ActivityType() { }
        #endregion
    }
}