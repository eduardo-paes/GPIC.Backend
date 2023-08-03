using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Domain.UseCases.Ports.StudentDocuments
{
    public class CreateStudentDocumentsInput
    {
        [Required]
        public Guid? ProjectId { get; set; }

        #region Documents
        /// <summary>
        /// Cópia do documento de identidade com foto.
        /// </summary>
        [Required]
        public IFormFile? IdentityDocument { get; set; }

        /// <summary>
        /// Cópia do CPF
        /// </summary>
        [Required]
        public IFormFile? CPF { get; set; }

        /// <summary>
        /// Foto 3x4
        /// </summary>
        [Required]
        public IFormFile? Photo3x4 { get; set; }

        /// <summary>
        /// Cópia atualizada do Histórico Escolar
        /// </summary>
        [Required]
        public IFormFile? SchoolHistory { get; set; }

        /// <summary>
        /// Termo de Compromisso do Bolsista assinado (Anexo II ou disponível na página do PIBIC) no caso de bolsista do CEFET/RJ
        /// </summary>
        [Required]
        public IFormFile? ScholarCommitmentAgreement { get; set; }

        /// <summary>
        /// Autorização dos pais ou responsáveis legais, em caso de aluno menor de 18 anos (Anexo 3 do Edital PIBIC ou modelo disponível na página da COPET)
        /// </summary>
        public IFormFile? ParentalAuthorization { get; set; }
        #endregion Documents

        #region BankData
        /// <summary>
        /// Número da Agência
        /// </summary>
        public string? AgencyNumber { get; set; }
        /// <summary>
        /// Número da Conta Corrente
        /// </summary>
        public string? AccountNumber { get; set; }

        /// <summary>
        /// Comprovante de Abertura de Conta
        /// </summary>
        [Required]
        public IFormFile? AccountOpeningProof { get; set; }
        #endregion BankData
    }
}