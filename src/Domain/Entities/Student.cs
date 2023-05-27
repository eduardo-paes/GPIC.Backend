using Domain.Entities.Primitives;
using Domain.Entities.Enums;
using Domain.Validation;

namespace Domain.Entities
{
    /// <summary>
    /// Estudante
    /// </summary>
    public class Student : Entity
    {
        #region Properties
        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                EntityExceptionValidation.When(value == default,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value >= DateTime.Now,
                    ExceptionMessageFactory.LessThan(nameof(value), DateTime.Now.ToString("dd/MM/yyyy")));
                _birthDate = value;
            }
        }

        private long _rg;
        public long RG
        {
            get { return _rg; }
            set
            {
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Required(nameof(value)));
                _rg = value;
            }
        }

        private string? _issuingAgency;
        /// <summary>
        /// Órgão emissor da identidade
        /// </summary>
        public string? IssuingAgency
        {
            get { return _issuingAgency; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value?.Length > 50,
                    ExceptionMessageFactory.MaxLength(nameof(value), 50));
                _issuingAgency = value;
            }
        }

        private DateTime _dispatchDate;
        /// <summary>
        /// Data Expedição da identidade
        /// </summary>
        public DateTime DispatchDate
        {
            get { return _dispatchDate; }
            set
            {
                EntityExceptionValidation.When(value == default,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value >= DateTime.Now,
                    ExceptionMessageFactory.LessThan(nameof(value), DateTime.Now.ToString("dd/MM/yyyy")));
                _dispatchDate = value;
            }
        }

        /// <summary>
        /// Sexo: Masculino e Feminino
        /// </summary>
        private EGender? _gender;
        public EGender? Gender
        {
            get { return _gender; }
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(!Enum.TryParse<EGender>(value.ToString(), out var _),
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _gender = value;
            }
        }

        private ERace? _race;
        /// <summary>
        /// Cor/Raça: Branca, Preta, Parda, Amarela, Indígena, Não declarado, Não dispõe da informação
        /// </summary>
        public ERace? Race
        {
            get { return _race; }
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(!Enum.TryParse<ERace>(value.ToString(), out var _),
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _race = value;
            }
        }

        private string? _homeAddress;
        /// <summary>
        /// Endereço Residencial (Rua, Avenida, N.º, complemento, bairro)
        /// </summary>
        public string? HomeAddress
        {
            get { return _homeAddress; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value?.Length > 300,
                    ExceptionMessageFactory.MaxLength(nameof(value), 300));
                _homeAddress = value;
            }
        }

        private string? _city;
        public string? City
        {
            get { return _city; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value?.Length > 50,
                    ExceptionMessageFactory.MaxLength(nameof(value), 50));
                _city = value;
            }
        }

        private string? _uf;
        public string? UF
        {
            get { return _uf; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required(nameof(value)));
                EntityExceptionValidation.When(value?.Length != 2,
                    ExceptionMessageFactory.WithLength(nameof(value), 2));
                _uf = value;
            }
        }

        private long _cep;
        public long CEP
        {
            get { return _cep; }
            set
            {
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _cep = value;
            }
        }

        private int? _phoneDDD;
        public int? PhoneDDD
        {
            get { return _phoneDDD; }
            set
            {
                if (value == null) return;
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _phoneDDD = value;
            }
        }

        private long? _phone;
        public long? Phone
        {
            get { return _phone; }
            set
            {
                if (value == null) return;
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _phone = value;
            }
        }

        private int? _cellPhoneDDD;
        public int? CellPhoneDDD
        {
            get { return _cellPhoneDDD; }
            set
            {
                if (value == null) return;
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _cellPhoneDDD = value;
            }
        }

        private long? _cellPhone;
        public long? CellPhone
        {
            get { return _cellPhone; }
            set
            {
                if (value == null) return;
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid(nameof(value)));
                _cellPhone = value;
            }
        }

        private Guid? _campusId;
        public Guid? CampusId
        {
            get { return _campusId; }
            set
            {
                EntityExceptionValidation.When(value == null || value == Guid.Empty,
                    ExceptionMessageFactory.Required(nameof(value)));
                _campusId = value;
            }
        }

        private Guid? _courseId;
        public Guid? CourseId
        {
            get { return _courseId; }
            set
            {
                EntityExceptionValidation.When(value == null || value == Guid.Empty,
                    ExceptionMessageFactory.Required(nameof(value)));
                _courseId = value;
            }
        }

        private string? _startYear;
        /// <summary>
        /// Ano de entrada / Semestre
        /// </summary>
        public string? StartYear
        {
            get { return _startYear; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required(nameof(value)));
                _startYear = value;
            }
        }

        /// <summary>
        /// Tipo de Bolsa de Assistência Estudantil do aluno
        /// </summary>
        private Guid? _studentAssistanceScholarshipId;
        public Guid? StudentAssistanceScholarshipId
        {
            get { return _studentAssistanceScholarshipId; }
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(value)));
                _studentAssistanceScholarshipId = value;
            }
        }

        /// <summary>
        /// ID do usuário
        /// </summary>
        private Guid? _userId;
        public Guid? UserId
        {
            get { return _userId; }
            set
            {
                {
                    EntityExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required(nameof(value)));
                    _userId = value;
                }
            }
        }

        public virtual User? User { get; }
        public virtual Campus? Campus { get; }
        public virtual Course? Course { get; }
        public virtual StudentAssistanceScholarship? StudentAssistanceScholarship { get; }
        #endregion

        #region Constructors
        public Student(
            DateTime birthDate,
            long rg,
            string issuingAgency,
            DateTime dispatchDate,
            EGender? gender,
            ERace? race,
            string homeAddress,
            string city,
            string uf,
            long cep,
            int? phoneDDD,
            long? phone,
            int? cellPhoneDDD,
            long? cellPhone,
            Guid? campusId,
            Guid? courseId,
            string startYear,
            Guid? studentAssistanceProgramId,
            Guid? userId)
        {
            BirthDate = birthDate;
            RG = rg;
            IssuingAgency = issuingAgency;
            DispatchDate = dispatchDate;
            Gender = gender;
            Race = race;
            HomeAddress = homeAddress;
            City = city;
            UF = uf;
            CEP = cep;
            PhoneDDD = phoneDDD;
            Phone = phone;
            CellPhoneDDD = cellPhoneDDD;
            CellPhone = cellPhone;
            CampusId = campusId;
            CourseId = courseId;
            StartYear = startYear;
            StudentAssistanceScholarshipId = studentAssistanceProgramId;
            UserId = userId;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        public Student() { }
        #endregion
    }
}