using System;
using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    public class StudentAssistanceScholarship : Entity
    {
        public string? Name { get; private set; }
        public string? Description { get; private set; }

        public StudentAssistanceScholarship(string name, string description)
        {
            ValidateDomain(name, description);
        }

        private void ValidateDomain(string name, string description)
        {
            // Name
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                ExceptionMessageFactory.Required("name"));
            DomainExceptionValidation.When(name.Length < 3,
                ExceptionMessageFactory.MinLength("name", 3));
            DomainExceptionValidation.When(name.Length > 100,
                ExceptionMessageFactory.MaxLength("name", 100));

            // Code
            DomainExceptionValidation.When(string.IsNullOrEmpty(description),
                ExceptionMessageFactory.Required("description"));
            DomainExceptionValidation.When(description.Length < 3,
                ExceptionMessageFactory.MinLength("description", 3));
            DomainExceptionValidation.When(description.Length > 1500,
                ExceptionMessageFactory.MaxLength("description", 1500));

            Name = name;
            Description = description;
        }
    }
}

