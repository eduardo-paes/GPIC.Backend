using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    public class StudentDocuments : Entity
    {
        #region Properties
        private Guid? _projectId;
        public Guid? ProjectId
        {
            get => _projectId;
            set
            {
                EntityExceptionValidation.When(value is null,
                    ExceptionMessageFactory.Required(nameof(ProjectId)));
                _projectId = value;
            }
        }

        #region Documents
        private string? _identityDocument;
        /// <summary>
        /// Cópia do documento de identidade com foto.
        /// </summary>
        public string? IdentityDocument
        {
            get => _identityDocument;
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required(nameof(IdentityDocument)));
                _identityDocument = value;
            }
        }

        private string? _cpf;
        /// <summary>
        /// Cópia do CPF
        /// </summary>
        public string? CPF
        {
            get => _cpf;
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required(nameof(CPF)));
                _cpf = value;
            }
        }

        private string? _photo3x4;
        /// <summary>
        /// Foto 3x4
        /// </summary>
        public string? Photo3x4
        {
            get => _photo3x4;
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required(nameof(Photo3x4)));
                _photo3x4 = value;
            }
        }

        private string? _schoolHistory;
        /// <summary>
        /// Cópia atualizada do Histórico Escolar
        /// </summary>
        public string? SchoolHistory
        {
            get => _schoolHistory;
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required(nameof(SchoolHistory)));
                _schoolHistory = value;
            }
        }

        private string? _scholarCommitmentAgreement;
        /// <summary>
        /// Termo de Compromisso do Bolsista assinado (Anexo II ou disponível na página do PIBIC) no caso de bolsista do CEFET/RJ
        /// </summary>
        public string? ScholarCommitmentAgreement
        {
            get => _scholarCommitmentAgreement;
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required(nameof(ScholarCommitmentAgreement)));
                _scholarCommitmentAgreement = value;
            }
        }

        private string? _parentalAuthorization;
        /// <summary>
        /// Autorização dos pais ou responsáveis legais, em caso de aluno menor de 18 anos (Anexo 3 do Edital PIBIC ou modelo disponível na página da COPET)
        /// </summary>
        public string? ParentalAuthorization
        {
            get => _parentalAuthorization;
            set
            {
                if (Project?.Student?.BirthDate >= DateTime.UtcNow.AddYears(-18))
                {
                    EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                        ExceptionMessageFactory.Required(nameof(ParentalAuthorization)));
                }
                _parentalAuthorization = value;
            }
        }
        #endregion Documents

        #region BankData
        private string? _agencyNumber;
        /// <summary>
        /// Número da Agência
        /// </summary>
        public string? AgencyNumber
        {
            get => _agencyNumber;
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required(nameof(AgencyNumber)));
                EntityExceptionValidation.When(long.TryParse(value, out long tmp) && tmp <= 0,
                    ExceptionMessageFactory.Invalid(ExceptionMessageFactory.Invalid(nameof(AgencyNumber))));
                _agencyNumber = value;
            }
        }

        private string? _accountNumber;
        /// <summary>
        /// Número da Conta Corrente
        /// </summary>
        public string? AccountNumber
        {
            get => _accountNumber;
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required(nameof(AccountNumber)));
                EntityExceptionValidation.When(long.TryParse(value, out long tmp) && tmp <= 0,
                    ExceptionMessageFactory.Invalid(ExceptionMessageFactory.Invalid(nameof(AccountNumber))));
                _accountNumber = value;
            }
        }

        private string? _accountOpeningProof;
        /// <summary>
        /// Comprovante de Abertura de Conta
        /// </summary>
        public string? AccountOpeningProof
        {
            get => _accountOpeningProof;
            set
            {
                EntityExceptionValidation.When(string.IsNullOrEmpty(value),
                    ExceptionMessageFactory.Required(nameof(AccountOpeningProof)));
                _accountOpeningProof = value;
            }
        }
        #endregion BankData

        public virtual Project? Project { get; }
        #endregion Properties

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        protected StudentDocuments() { }

        public StudentDocuments(Guid? projectId, string? agencyNumber, string? accountNumber)
        {
            ProjectId = projectId;
            AgencyNumber = agencyNumber;
            AccountNumber = accountNumber;
        }
    }
}