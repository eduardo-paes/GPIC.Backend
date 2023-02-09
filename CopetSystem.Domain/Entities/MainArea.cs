using System;
using CopetSystem.Domain.Validation;

namespace CopetSystem.Domain.Entities
{
	public class MainArea : Entity
    {
        #region Attributes
        private string? _code;
        public string? Code
        {
            get { return _code; }
            private set
            {
                DomainExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("code"));
                DomainExceptionValidation.When(value.Length < 3,
                    ExceptionMessageFactory.MinLength("code", 3));
                DomainExceptionValidation.When(value.Length > 100,
                    ExceptionMessageFactory.MaxLength("code", 100));
                _code = value;
            }
        }

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
        #endregion

        #region Constructors
        public MainArea(string code, string name)
        {
            Code = code;
            Name = name;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        protected MainArea() { }
        #endregion
    }
}

