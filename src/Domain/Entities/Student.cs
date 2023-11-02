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
        private string? _registrationCode;
        /// <summary>
        /// Código de Matrícula
        /// </summary>
        public string? RegistrationCode
        {
            get { return _registrationCode; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required(nameof(RegistrationCode)));
                EntityExceptionValidation.When(value?.Length > 20,
                    ExceptionMessageFactory.MaxLength(nameof(RegistrationCode), 20));
                _registrationCode = value?.ToUpper();
            }
        }


        private DateTime _birthDate;
        /// <summary>
        /// Data de Nascimento
        /// </summary>
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                EntityExceptionValidation.When(value == default,
                    ExceptionMessageFactory.Required(nameof(BirthDate)));
                EntityExceptionValidation.When(value >= DateTime.UtcNow,
                    ExceptionMessageFactory.LessThan(nameof(BirthDate), DateTime.UtcNow.ToString("dd/MM/yyyy")));
                _birthDate = value.ToUniversalTime();
            }
        }

        private long _rg;
        /// <summary>
        /// RG
        /// </summary>
        public long RG
        {
            get { return _rg; }
            set
            {
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Required(nameof(RG)));
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
                    ExceptionMessageFactory.Required(nameof(IssuingAgency)));
                EntityExceptionValidation.When(value?.Length > 50,
                    ExceptionMessageFactory.MaxLength(nameof(IssuingAgency), 50));
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
                    ExceptionMessageFactory.Required(nameof(DispatchDate)));
                EntityExceptionValidation.When(value >= DateTime.UtcNow,
                    ExceptionMessageFactory.LessThan(nameof(DispatchDate),
                    DateTime.UtcNow.ToString("dd/MM/yyyy")));
                _dispatchDate = value.ToUniversalTime();
            }
        }

        private EGender? _gender;
        /// <summary>
        /// Sexo: Masculino e Feminino
        /// </summary>
        public EGender? Gender
        {
            get { return _gender; }
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(Gender)));
                EntityExceptionValidation.When(!Enum.TryParse<EGender>(value.ToString(), out var _),
                    ExceptionMessageFactory.Invalid(nameof(Gender)));
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
                    ExceptionMessageFactory.Required(nameof(Race)));
                EntityExceptionValidation.When(!Enum.TryParse<ERace>(value.ToString(), out var _),
                    ExceptionMessageFactory.Invalid(nameof(Race)));
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
                    ExceptionMessageFactory.Required(nameof(HomeAddress)));
                EntityExceptionValidation.When(value?.Length > 300,
                    ExceptionMessageFactory.MaxLength(nameof(HomeAddress), 300));
                _homeAddress = value;
            }
        }

        private string? _city;
        /// <summary>
        /// Cidade
        /// </summary>
        public string? City
        {
            get { return _city; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required(nameof(City)));
                EntityExceptionValidation.When(value?.Length > 50,
                    ExceptionMessageFactory.MaxLength(nameof(City), 50));
                _city = value;
            }
        }

        private string? _uf;
        /// <summary>
        /// Estado
        /// </summary>
        public string? UF
        {
            get { return _uf; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required(nameof(UF)));
                EntityExceptionValidation.When(value?.Length != 2,
                    ExceptionMessageFactory.WithLength(nameof(UF), 2));
                _uf = value;
            }
        }

        private long _cep;
        /// <summary>
        /// CEP
        /// </summary>
        public long CEP
        {
            get { return _cep; }
            set
            {
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid(nameof(CEP)));
                _cep = value;
            }
        }

        private int? _phoneDDD;
        /// <summary>
        /// DDD Telefone
        /// </summary>
        public int? PhoneDDD
        {
            get { return _phoneDDD; }
            set
            {
                if (value == null) return;
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid(nameof(PhoneDDD)));
                _phoneDDD = value;
            }
        }

        private long? _phone;
        /// <summary>
        /// Telefone
        /// </summary>
        public long? Phone
        {
            get { return _phone; }
            set
            {
                if (value == null) return;
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid(nameof(Phone)));
                _phone = value;
            }
        }

        private int? _cellPhoneDDD;
        /// <summary>
        /// DDD Celular
        /// </summary>
        public int? CellPhoneDDD
        {
            get { return _cellPhoneDDD; }
            set
            {
                if (value == null) return;
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid(nameof(CellPhoneDDD)));
                _cellPhoneDDD = value;
            }
        }

        private long? _cellPhone;
        /// <summary>
        /// Celular
        /// </summary>
        public long? CellPhone
        {
            get { return _cellPhone; }
            set
            {
                if (value == null) return;
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid(nameof(CellPhone)));
                _cellPhone = value;
            }
        }

        private Guid? _campusId;
        /// <summary>
        /// ID do Campus
        /// </summary>
        public Guid? CampusId
        {
            get { return _campusId; }
            set
            {
                EntityExceptionValidation.When(value == null || value == Guid.Empty,
                    ExceptionMessageFactory.Required(nameof(CampusId)));
                _campusId = value;
            }
        }

        private Guid? _courseId;
        /// <summary>
        /// ID do Curso
        /// </summary>
        public Guid? CourseId
        {
            get { return _courseId; }
            set
            {
                EntityExceptionValidation.When(value == null || value == Guid.Empty,
                    ExceptionMessageFactory.Required(nameof(CourseId)));
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
                    ExceptionMessageFactory.Required(nameof(StartYear)));
                _startYear = value;
            }
        }

        private Guid? _assistanceTypeId;
        /// <summary>
        /// Tipo de Bolsa de Assistência Estudantil do aluno
        /// </summary>
        public Guid? AssistanceTypeId
        {
            get { return _assistanceTypeId; }
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required(nameof(AssistanceTypeId)));
                _assistanceTypeId = value;
            }
        }

        private Guid? _userId;
        /// <summary>
        /// ID do usuário
        /// </summary>
        public Guid? UserId
        {
            get { return _userId; }
            set
            {
                {
                    EntityExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required(nameof(UserId)));
                    _userId = value;
                }
            }
        }

        public virtual User? User { get; }
        public virtual Campus? Campus { get; }
        public virtual Course? Course { get; }
        public virtual AssistanceType? AssistanceType { get; }
        #endregion

        #region Constructors
        public Student(
            DateTime birthDate,
            long rg,
            string? issuingAgency,
            DateTime dispatchDate,
            EGender? gender,
            ERace? race,
            string? homeAddress,
            string? city,
            string? uf,
            long cep,
            int? phoneDDD,
            long? phone,
            int? cellPhoneDDD,
            long? cellPhone,
            Guid? campusId,
            Guid? courseId,
            string? startYear,
            Guid? studentAssistanceProgramId,
            string? registrationCode)
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
            AssistanceTypeId = studentAssistanceProgramId;
            RegistrationCode = registrationCode;
        }

        public Student(
            Guid id,
            DateTime birthDate,
            long rg,
            string? issuingAgency,
            DateTime dispatchDate,
            EGender? gender,
            ERace? race,
            string? homeAddress,
            string? city,
            string? uf,
            long cep,
            int? phoneDDD,
            long? phone,
            int? cellPhoneDDD,
            long? cellPhone,
            Guid? campusId,
            Guid? courseId,
            string? startYear,
            Guid? studentAssistanceProgramId,
            string? registrationCode)
        {
            Id = id;
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
            AssistanceTypeId = studentAssistanceProgramId;
            RegistrationCode = registrationCode;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        protected Student() { }
        #endregion
    }
}