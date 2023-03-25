using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    /// <summary>
    /// Bolsa de Assistência Estudantil
    /// </summary>
    public class StudentAssistanceScholarship : Entity
    {
        private string? _name;
        public string? Name
        {
            get { return _name; }
            private set
            {
                DomainExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("nome"));
                DomainExceptionValidation.When(value.Length < 3,
                    ExceptionMessageFactory.MinLength("nome", 3));
                DomainExceptionValidation.When(value.Length > 100,
                    ExceptionMessageFactory.MaxLength("nome", 100));
                _name = value;
            }
        }

        private string? _description;
        public string? Description
        {
            get { return _description; }
            private set
            {
                DomainExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("descrição"));
                DomainExceptionValidation.When(value.Length < 3,
                    ExceptionMessageFactory.MinLength("descrição", 3));
                DomainExceptionValidation.When(value.Length > 1500,
                    ExceptionMessageFactory.MaxLength("descrição", 1500));
                _description = value;
            }
        }

        public StudentAssistanceScholarship(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}