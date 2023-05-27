using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    public class SubArea : Entity
    {
        #region Properties
        private Guid? _areaId;
        public Guid? AreaId
        {
            get { return _areaId; }
            set
            {
                EntityExceptionValidation.When(!value.HasValue,
                    ExceptionMessageFactory.Invalid("areaId"));
                _areaId = value;
            }
        }
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
        public virtual Area? Area { get; }
        #endregion

        #region Constructors
        public SubArea(Guid? areaId, string? code, string? name)
        {
            AreaId = areaId;
            Code = code;
            Name = name;
        }

        public SubArea(Guid id, Guid? areaId, string? code, string? name, Area? area = null)
        {
            Id = id;
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
    }
}