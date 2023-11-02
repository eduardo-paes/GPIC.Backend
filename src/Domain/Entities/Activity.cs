using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    public class Activity : Entity
    {
        #region Properties
        private string? _name;
        /// <summary>
        /// Nome da atividade
        /// </summary>
        public string? Name
        {
            get => _name;
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

        private double? _points;
        /// <summary>
        /// Pontuação da atividade
        /// </summary>
        public double? Points
        {
            get => _points;
            set
            {
                EntityExceptionValidation.When(value is null,
                    ExceptionMessageFactory.Required(nameof(Points)));
                _points = value;
            }
        }

        private double? _limits;
        /// <summary>
        /// Limite de pontuação da atividade
        /// </summary>
        public double? Limits
        {
            get => _limits;
            set => _limits = value ?? double.MaxValue;
        }

        private Guid? _activityTypeId;
        /// <summary>
        /// Id do tipo de atividade
        /// </summary>
        public Guid? ActivityTypeId
        {
            get => _activityTypeId;
            private set
            {
                {
                    EntityExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required(nameof(ActivityTypeId)));
                    _activityTypeId = value;
                }
            }
        }

        public virtual ActivityType? ActivityType { get; }
        #endregion

        #region Constructors
        public Activity(string? name, double? points, double? limits, Guid? activityTypeId)
        {
            Name = name;
            Points = points;
            Limits = limits;
            ActivityTypeId = activityTypeId;
        }

        public Activity(Guid? id, string? name, double? points, double? limits, Guid? activityTypeId)
        {
            Id = id;
            Name = name;
            Points = points;
            Limits = limits;
            ActivityTypeId = activityTypeId;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        protected Activity() { }
        #endregion
    }
}