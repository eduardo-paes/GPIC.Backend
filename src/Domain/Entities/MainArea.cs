﻿using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    /// <summary>
    /// Grande área de conhecimento
    /// </summary>
    public class MainArea : Entity
    {
        #region Properties
        private string? _code;
        public string? Code
        {
            get { return _code; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("code"));
                EntityExceptionValidation.When(value?.Length < 3,
                    ExceptionMessageFactory.MinLength("code", 3));
                EntityExceptionValidation.When(value?.Length > 100,
                    ExceptionMessageFactory.MaxLength("code", 100));
                _code = value;
            }
        }

        private string? _name;
        public string? Name
        {
            get { return _name; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                ExceptionMessageFactory.Required("name"));
                EntityExceptionValidation.When(value?.Length < 3,
                    ExceptionMessageFactory.MinLength("name", 3));
                EntityExceptionValidation.When(value?.Length > 300,
                    ExceptionMessageFactory.MaxLength("name", 300));
                _name = value;
            }
        }
        #endregion

        #region Constructors
        public MainArea(Guid id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

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