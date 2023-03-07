using CopetSystem.Domain.Entities.Primitives;
using CopetSystem.Domain.Validation;

namespace CopetSystem.Domain.Entities
{
    public class SubArea : Entity
    {
        #region Attributes
        private Guid? _areaId;
        public Guid? AreaId
        {
            get { return _areaId; }
            private set
            {
                DomainExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Invalid("areaId"));
                _areaId = value;
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
        public virtual Area? Area { get; private set; }
        #endregion

        #region Constructors
        public SubArea(Guid? areaId, string? code, string? name)
        {
            AreaId = areaId;
            Code = code;
            Name = name;
        }

        public SubArea(Guid? areaId, string? code, string? name, Area? area)
        {
            AreaId = areaId;
            Code = code;
            Name = name;
            Area = area;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        protected SubArea() { }
        #endregion

        #region Updaters
        public void UpdateName(string? name) => Name = name;
        public void UpdateCode(string? code) => Code = code;
        public void UpdateArea(Guid? areaId) => AreaId = areaId;
        #endregion
    }
}