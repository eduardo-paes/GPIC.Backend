namespace Application.Ports.StudentDocuments
{
    public abstract class BaseStudentDocumentsOutput
    {
        public Guid? Id { get; set; }
        public Guid? ProjectId { get; set; }

        #region Documents
        /// <summary>
        /// Cópia do documento de identidade com foto.
        /// </summary>
        public string? IdentityDocument { get; set; }

        /// <summary>
        /// Cópia do CPF
        /// </summary>
        public string? CPF { get; set; }

        /// <summary>
        /// Foto 3x4
        /// </summary>
        public string? Photo3x4 { get; set; }

        /// <summary>
        /// Cópia atualizada do Histórico Escolar
        /// </summary>
        public string? SchoolHistory { get; set; }

        /// <summary>
        /// Termo de Compromisso do Bolsista assinado (Anexo II ou disponível na página do PIBIC) no caso de bolsista do CEFET/RJ
        /// </summary>
        public string? ScholarCommitmentAgreement { get; set; }

        /// <summary>
        /// Autorização dos pais ou responsáveis legais, em caso de aluno menor de 18 anos (Anexo 3 do Edital PIBIC ou modelo disponível na página da COPET)
        /// </summary>
        public string? ParentalAuthorization { get; set; }
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
        public string? AccountOpeningProof { get; set; }
        #endregion BankData
    }
}