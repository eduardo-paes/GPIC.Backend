using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    /// <summary>
    /// Tipo de Programa
    /// </summary>
    public class ProgramType : Entity
    {
        private string? _name;
        public string? Name
        {
            get { return _name; }
            set
            {
                DomainExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("nome"));
                DomainExceptionValidation.When(value?.Length < 3,
                    ExceptionMessageFactory.MinLength("nome", 3));
                DomainExceptionValidation.When(value?.Length > 300,
                    ExceptionMessageFactory.MaxLength("nome", 300));
                _name = value;
            }
        }

        private string? _description;
        public string? Description
        {
            get { return _description; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    DomainExceptionValidation.When(value?.Length < 3,
                        ExceptionMessageFactory.MinLength("descrição", 3));
                    DomainExceptionValidation.When(value?.Length > 300,
                        ExceptionMessageFactory.MaxLength("descrição", 300));
                }
                _description = value;
            }
        }

        public ProgramType(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        public ProgramType() { }
    }
}