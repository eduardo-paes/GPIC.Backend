using System;
using System.Xml.Linq;
using CopetSystem.Domain.Validation;

namespace CopetSystem.Domain.Entities
{
    public class Area : Entity
    {
        public long MainAreaId { get; private set; }
        public string? Code { get; private set; }
        public string? Name { get; private set; }

        public Area(long mainAreaId, string code, string name)
        {
            ValidateDomain(mainAreaId, code, name);
        }

        private void ValidateDomain(long mainAreaId, string code, string name)
        {
            // Id
            DomainExceptionValidation.When(mainAreaId < 0,
                ExceptionMessageFactory.Invalid("id"));

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

            MainAreaId = mainAreaId;
            Code = code;
            Name = name;
        }
    }
}

