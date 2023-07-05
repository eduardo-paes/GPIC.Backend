using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    /// <summary>
    /// Tipo de Bolsa de Assistência Estudantil
    /// </summary>
    public class TypeAssistance : Entity
    {
        private string? _name;
        public string? Name
        {
            get { return _name; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("nome"));
                EntityExceptionValidation.When(value?.Length < 3,
                    ExceptionMessageFactory.MinLength("nome", 3));
                EntityExceptionValidation.When(value?.Length > 100,
                    ExceptionMessageFactory.MaxLength("nome", 100));
                _name = value;
            }
        }

        private string? _description;
        public string? Description
        {
            get { return _description; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("descrição"));
                EntityExceptionValidation.When(value?.Length < 3,
                    ExceptionMessageFactory.MinLength("descrição", 3));
                EntityExceptionValidation.When(value?.Length > 1500,
                    ExceptionMessageFactory.MaxLength("descrição", 1500));
                _description = value;
            }
        }

        public TypeAssistance(string? name, string? description)
        {
            Name = name;
            Description = description;
        }

        public TypeAssistance(Guid? id, string? name, string? description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        public TypeAssistance() { }
    }
}