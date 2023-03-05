using System;
using System.Xml.Linq;
using CopetSystem.Domain.Entities.Primitives;
using CopetSystem.Domain.Validation;

namespace CopetSystem.Domain.Entities
{
    public class Area : Entity
    {
        #region Attributes
        private Guid? _mainAreaId;
        public Guid? MainAreaId
        {
            get { return _mainAreaId; }
            private set
            {
                DomainExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Invalid("mainAreaId"));
                _mainAreaId = value;
            }
        }

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

        public virtual MainArea? MainArea { get; private set; }
        #endregion

        #region Constructors
        public Area(Guid? mainAreaId, string? code, string? name)
        {
            MainAreaId = mainAreaId;
            Code = code;
            Name = name;
        }

        public Area(Guid? mainAreaId, string? code, string? name, MainArea? mainArea)
        {
            MainAreaId = mainAreaId;
            Code = code;
            Name = name;
            MainArea = mainArea;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        protected Area() { }
        #endregion

        #region Updaters
        public void UpdateName(string? name) => Name = name;
        public void UpdateCode(string? code) => Code = code;
        public void UpdateMainArea(Guid? mainAreaId) => MainAreaId = mainAreaId;
        #endregion
    }
}