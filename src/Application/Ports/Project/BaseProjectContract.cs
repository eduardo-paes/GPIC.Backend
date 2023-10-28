namespace Application.Ports.Project
{
    public abstract class BaseProjectContract
    {
        #region Informações do Projeto
        public string? Title { get; set; }
        public string? KeyWord1 { get; set; }
        public string? KeyWord2 { get; set; }
        public string? KeyWord3 { get; set; }
        public bool IsScholarshipCandidate { get; set; }
        public string? Objective { get; set; }
        public string? Methodology { get; set; }
        public string? ExpectedResults { get; set; }
        public string? ActivitiesExecutionSchedule { get; set; }
        #endregion Informações do Projeto

        #region Resultados da Avaliação
        public int? Status { get; set; }
        public string? StatusDescription { get; set; }
        public string? EvaluatorObservation { get; set; }
        public string? AppealDescription { get; set; }
        public string? AppealEvaluatorObservation { get; set; }
        #endregion Resultados da Avaliação

        #region Relacionamentos
        public Guid? ProgramTypeId { get; set; }
        public Guid? ProfessorId { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? SubAreaId { get; set; }
        public Guid? NoticeId { get; set; }
        #endregion Relacionamentos

        #region Informações de Controle
        public DateTime? SubmitionDate { get; set; }
        public DateTime? RessubmitionDate { get; set; }
        public DateTime? CancellationDate { get; set; }
        public string? CancellationReason { get; set; }
        public DateTime SendingDocumentationDeadline { get; set; }
        #endregion Informações de Controle

        #region Informações dos Envolvidos
        public string? ProfessorName { get; set; }
        public string? StudentName { get; set; }
        #endregion Informações dos Envolvidos
    }
}