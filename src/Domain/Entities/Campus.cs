using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    public class Campus : Entity
    {
        private string? _name;
        public string? Name
        {
            get { return _name; }
            private set
            {
                DomainExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("name"));
                DomainExceptionValidation.When(value.Length < 3,
                    ExceptionMessageFactory.MinLength("name", 3));
                DomainExceptionValidation.When(value.Length > 300,
                    ExceptionMessageFactory.MaxLength("name", 300));
                _name = value;
            }
        }

        public Campus(string? name)
        {
            Name = name;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        public Campus() { }
    }
}