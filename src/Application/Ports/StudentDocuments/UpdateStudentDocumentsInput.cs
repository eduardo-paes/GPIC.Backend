using Microsoft.AspNetCore.Http;

namespace Application.Ports.StudentDocuments
{
    public class UpdateStudentDocumentsInput
    {
        /// <summary>
        /// Cópia do documento de identidade com foto.
        /// </summary>
        public IFormFile? IdentityDocument { get; set; }

        /// <summary>
        /// Cópia do CPF
        /// </summary>
        public IFormFile? CPF { get; set; }

        /// <summary>
        /// Foto 3x4
        /// </summary>
        public IFormFile? Photo3x4 { get; set; }

        /// <summary>
        /// Cópia atualizada do Histórico Escolar
        /// </summary>
        public IFormFile? SchoolHistory { get; set; }

        /// <summary>
        /// Termo de Compromisso do Bolsista assinado (Anexo II ou disponível na página do PIBIC) no caso de bolsista do CEFET/RJ
        /// </summary>
        public IFormFile? ScholarCommitmentAgreement { get; set; }

        /// <summary>
        /// Autorização dos pais ou responsáveis legais, em caso de aluno menor de 18 anos (Anexo 3 do Edital PIBIC ou modelo disponível na página da COPET)
        /// </summary>
        public IFormFile? ParentalAuthorization { get; set; }

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
        public IFormFile? AccountOpeningProof { get; set; }
    }
}