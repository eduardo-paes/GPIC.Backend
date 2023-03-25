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
                DomainExceptionValidation.When(value == default,
                    ExceptionMessageFactory.Required("Data de Nascimento"));
                DomainExceptionValidation.When(value >= DateTime.Now,
                    ExceptionMessageFactory.LessThan("Data de Nascimento", "data atual"));
                _birthDate = value;
            }
        }

        private long _rg;
        public long RG
        {
            get { return _rg; }
            set
            {
                DomainExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Required("RG"));
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
                DomainExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("Órgão Emissor"));
                DomainExceptionValidation.When(value.Length > 50,
                    ExceptionMessageFactory.MaxLength("Órgão Emissor", 50));
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
                DomainExceptionValidation.When(value == default,
                    ExceptionMessageFactory.Required("Data de expedição da identidade"));
                DomainExceptionValidation.When(value >= DateTime.Now,
                    ExceptionMessageFactory.LessThan("Data de expedição da identidade", "data atual"));
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
                DomainExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required("Sexo"));
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
                DomainExceptionValidation.When(value == null,
                    ExceptionMessageFactory.Required("Cor/Raça"));
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
                DomainExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("Endereço Residencial"));
                DomainExceptionValidation.When(value.Length > 300,
                    ExceptionMessageFactory.MaxLength("Endereço Residencial", 300));
                _homeAddress = value;
            }
        }

        private string? _city;
        public string? City
        {
            get { return _city; }
            set
            {
                DomainExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("Cidade"));
                DomainExceptionValidation.When(value.Length > 50,
                    ExceptionMessageFactory.MaxLength("Cidade", 50));
                _city = value;
            }
        }

        private string? _uf;
        public string? UF
        {
            get { return _uf; }
            set
            {
                DomainExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("UF"));
                DomainExceptionValidation.When(value.Length != 2,
                    ExceptionMessageFactory.WithLength("UF", 2));
                _uf = value;
            }
        }

        private long _cep;
        public long CEP
        {
            get { return _cep; }
            set
            {
                DomainExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid("CEP"));
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
                DomainExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid("DDD do telefone"));
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
                DomainExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid("telefone"));
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
                DomainExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid("DDD do celular"));
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
                DomainExceptionValidation.When(value <= 0,
                    ExceptionMessageFactory.Invalid("celular"));
                _cellPhone = value;
            }
        }

        private Guid? _campusId;
        public Guid? CampusId
        {
            get { return _campusId; }
            set
            {
                DomainExceptionValidation.When(value == null || value == Guid.Empty,
                    ExceptionMessageFactory.Required("campus"));
                _campusId = value;
            }
        }

        private Guid? _courseId;
        public Guid? CourseId
        {
            get { return _courseId; }
            set
            {
                DomainExceptionValidation.When(value == null || value == Guid.Empty,
                    ExceptionMessageFactory.Required("course"));
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
                DomainExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required("ano de entrada"));
                _startYear = value;
            }
        }

        /// <summary>
        /// O aluno possui bolsa de assistência estudantil
        /// </summary>
        private EStudentAssistanceProgram? _studentAssistanceProgram;
        public EStudentAssistanceProgram? StudentAssistanceProgram
        {
            get { return _studentAssistanceProgram; }
            set
            {
                {
                    DomainExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required("bolsa de estudos"));
                    _studentAssistanceProgram = value;
                }
            }
        }

        /// <summary>
        /// ID do usuário
        /// </summary>
        private Guid? _userId;
        public Guid? UserId
        {
            get { return _userId; }
            private set
            {
                {
                    DomainExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required("id do usuário"));
                    _userId = value;
                }
            }
        }

        public virtual User? User { get; }
        public virtual Campus? Campus { get; }
        public virtual Course? Course { get; }
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
            EStudentAssistanceProgram? studentAssistanceProgram,
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
            StudentAssistanceProgram = studentAssistanceProgram;
            UserId = userId;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        public Student() { }
        #endregion
    }
}