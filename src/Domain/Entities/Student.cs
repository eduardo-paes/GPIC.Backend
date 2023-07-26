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
        /// <summary>
        /// Data de Nascimento
        /// </summary>
        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                EntityExceptionValidation.When(value == default,
                    ExceptionMessageFactory.Required("Data de Nascimento"));
                EntityExceptionValidation.When(value >= DateTime.UtcNow,
                    ExceptionMessageFactory.LessThan("Data de Nascimento", DateTime.UtcNow.ToString("dd/MM/yyyy")));
                _birthDate = value.ToUniversalTime();
            }
        }

        /// <summary>
        /// RG
        /// </summary>
        private long _rg;
        public long RG
        {
            get { return _rg; }
            set
            {
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Required("RG"));
                _rg = value;
            }
        }

        /// <summary>
        /// Órgão emissor da identidade
        /// </summary>
        private string? _issuingAgency;
        public string? IssuingAgency
        {
            get { return _issuingAgency; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("Órgão emissor da identidade"));
                EntityExceptionValidation.When(value?.Length > 50,
                    ExceptionMessageFactory.MaxLength("Órgão emissor da identidade", 50));
                _issuingAgency = value;
            }
        }

        /// <summary>
        /// Data Expedição da identidade
        /// </summary>
        private DateTime _dispatchDate;
        public DateTime DispatchDate
        {
            get { return _dispatchDate; }
            set
            {
                EntityExceptionValidation.When(value == default,
                    ExceptionMessageFactory.Required("Data Expedição da identidade"));
                EntityExceptionValidation.When(value >= DateTime.UtcNow,
                    ExceptionMessageFactory.LessThan("Data Expedição da identidade",
                    DateTime.UtcNow.ToString("dd/MM/yyyy")));
                _dispatchDate = value.ToUniversalTime();
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
                    ExceptionMessageFactory.Required("Sexo"));
                EntityExceptionValidation.When(!Enum.TryParse<EGender>(value.ToString(), out var _),
                    ExceptionMessageFactory.Invalid("Sexo"));
                _gender = value;
            }
        }

        /// <summary>
        /// Cor/Raça: Branca, Preta, Parda, Amarela, Indígena, Não declarado, Não dispõe da informação
        /// </summary>
        private ERace? _race;
        public ERace? Race
        {
            get { return _race; }
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required("Cor/Raça"));
                EntityExceptionValidation.When(!Enum.TryParse<ERace>(value.ToString(), out var _),
                    ExceptionMessageFactory.Invalid("Cor/Raça"));
                _race = value;
            }
        }

        /// <summary>
        /// Endereço Residencial (Rua, Avenida, N.º, complemento, bairro)
        /// </summary>
        private string? _homeAddress;
        public string? HomeAddress
        {
            get { return _homeAddress; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("Endereço Residencial"));
                EntityExceptionValidation.When(value?.Length > 300,
                    ExceptionMessageFactory.MaxLength("Endereço Residencial", 300));
                _homeAddress = value;
            }
        }

        /// <summary>
        /// Cidade
        /// </summary>
        private string? _city;
        public string? City
        {
            get { return _city; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("Cidade"));
                EntityExceptionValidation.When(value?.Length > 50,
                    ExceptionMessageFactory.MaxLength("Cidade", 50));
                _city = value;
            }
        }

        /// <summary>
        /// Estado
        /// </summary>
        private string? _uf;
        public string? UF
        {
            get { return _uf; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("Estado"));
                EntityExceptionValidation.When(value?.Length != 2,
                    ExceptionMessageFactory.WithLength("Estado", 2));
                _uf = value;
            }
        }

        /// <summary>
        /// CEP
        /// </summary>
        private long _cep;
        public long CEP
        {
            get { return _cep; }
            set
            {
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid("CEP"));
                _cep = value;
            }
        }

        /// <summary>
        /// DDD Telefone
        /// </summary>
        private int? _phoneDDD;
        public int? PhoneDDD
        {
            get { return _phoneDDD; }
            set
            {
                if (value == null) return;
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid("DDD Telefo"));
                _phoneDDD = value;
            }
        }

        /// <summary>
        /// Telefone
        /// </summary>
        private long? _phone;
        public long? Phone
        {
            get { return _phone; }
            set
            {
                if (value == null) return;
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid("Telefone"));
                _phone = value;
            }
        }

        /// <summary>
        /// DDD Celular
        /// </summary>
        private int? _cellPhoneDDD;
        public int? CellPhoneDDD
        {
            get { return _cellPhoneDDD; }
            set
            {
                if (value == null) return;
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid("DDD Celular"));
                _cellPhoneDDD = value;
            }
        }

        /// <summary>
        /// Celular
        /// </summary>
        private long? _cellPhone;
        public long? CellPhone
        {
            get { return _cellPhone; }
            set
            {
                if (value == null) return;
                EntityExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid("Celular"));
                _cellPhone = value;
            }
        }

        /// <summary>
        /// ID do Campus
        /// </summary>
        private Guid? _campusId;
        public Guid? CampusId
        {
            get { return _campusId; }
            set
            {
                EntityExceptionValidation.When(value == null || value == Guid.Empty,
                    ExceptionMessageFactory.Required("Campus"));
                _campusId = value;
            }
        }

        /// <summary>
        /// ID do Curso
        /// </summary>
        private Guid? _courseId;
        public Guid? CourseId
        {
            get { return _courseId; }
            set
            {
                EntityExceptionValidation.When(value == null || value == Guid.Empty,
                    ExceptionMessageFactory.Required("Curso"));
                _courseId = value;
            }
        }

        /// <summary>
        /// Ano de entrada / Semestre
        /// </summary>
        private string? _startYear;
        public string? StartYear
        {
            get { return _startYear; }
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("Ano de entrada / Semestre"));
                _startYear = value;
            }
        }

        /// <summary>
        /// Tipo de Bolsa de Assistência Estudantil do aluno
        /// </summary>
        private Guid? _assistanceTypeId;
        public Guid? AssistanceTypeId
        {
            get { return _assistanceTypeId; }
            set
            {
                EntityExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required("Tipo de Bolsa de Assistência Estudantil do aluno"));
                _assistanceTypeId = value;
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
                        ExceptionMessageFactory.Required("Usuário"));
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
            Guid? studentAssistanceProgramId)
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
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        protected Student() { }
        #endregion
    }
}