using System;
using CopetSystem.Domain.Validation;

namespace CopetSystem.Domain.Entities
{
	public class MainArea : Entity
    {
		public string? Code { get; private set; }
		public string? Name { get; private set; }

        public MainArea(string code, string name)
        {
            ValidateDomain(code, name);
        }

        private void ValidateDomain(string code, string name)
        {
            // Code
            DomainExceptionValidation.When(string.IsNullOrEmpty(code),
                ExceptionMessageFactory.Required("code"));
            DomainExceptionValidation.When(code.Length < 3,
                ExceptionMessageFactory.MinLength("code", 3));
            DomainExceptionValidation.When(code.Length > 100,
                ExceptionMessageFactory.MaxLength("code", 100));

            // Name
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                ExceptionMessageFactory.Required("name"));
            DomainExceptionValidation.When(name.Length < 3,
                ExceptionMessageFactory.MinLength("name", 3));
            DomainExceptionValidation.When(name.Length > 300,
                ExceptionMessageFactory.MaxLength("name", 300));

            Code = code;
            Name = name;
        }
    }
}

