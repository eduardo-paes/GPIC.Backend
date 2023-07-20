namespace Domain.Contracts.Project
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
        #endregion

        #region Produção Científica - Trabalhos Publicados
        public int? WorkType1 { get; set; }
        public int? WorkType2 { get; set; }
        public int? IndexedConferenceProceedings { get; set; }
        public int? NotIndexedConferenceProceedings { get; set; }
        public int? CompletedBook { get; set; }
        public int? OrganizedBook { get; set; }
        public int? BookChapters { get; set; }
        public int? BookTranslations { get; set; }
        public int? ParticipationEditorialCommittees { get; set; }
        #endregion

        #region Produção Artístca e Cultural - Produção Apresentada
        public int? FullComposerSoloOrchestraAllTracks { get; set; }
        public int? FullComposerSoloOrchestraCompilation { get; set; }
        public int? ChamberOrchestraInterpretation { get; set; }
        public int? IndividualAndCollectiveArtPerformances { get; set; }
        public int? ScientificCulturalArtisticCollectionsCuratorship { get; set; }
        #endregion

        #region Produção Técnica - Produtos Registrados
        public int? PatentLetter { get; set; }
        public int? PatentDeposit { get; set; }
        public int? SoftwareRegistration { get; set; }
        #endregion

        #region Resultados da Avaliação
        public int? Status { get; set; }
        public string? StatusDescription { get; set; }
        public string? EvaluatorObservation { get; set; }
        public string? AppealDescription { get; set; }
        public string? AppealEvaluatorObservation { get; set; }
        #endregion

        #region Relacionamentos
        public Guid? ProgramTypeId { get; set; }
        public Guid? ProfessorId { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? SubAreaId { get; set; }
        public Guid? NoticeId { get; set; }
        #endregion

        #region Informações de Controle
        public DateTime? SubmitionDate { get; set; }
        public DateTime? RessubmitionDate { get; set; }
        public DateTime? CancellationDate { get; set; }
        public string? CancellationReason { get; set; }
        public DateTime SendingDocumentationDeadline { get; set; }
        #endregion
    }
}