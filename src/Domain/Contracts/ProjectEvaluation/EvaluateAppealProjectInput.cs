namespace Domain.Contracts.ProjectEvaluation;
public class EvaluateAppealProjectInput
{
    #region Informações Gerais da Avaliação
    public Guid? ProjectId { get; set; }
    public bool IsProductivityFellow { get; set; }
    public Guid? AppealEvaluatorId { get; set; }
    public DateTime? AppealEvaluationDate { get; set; }
    public string? AppealEvaluationDescription { get; set; }
    #endregion

    #region (Resultados) Produção Científica - Trabalhos Publicados
    public int? FoundWorkType1 { get; set; }
    public int? FoundWorkType2 { get; set; }
    public int? FoundIndexedConferenceProceedings { get; set; }
    public int? FoundNotIndexedConferenceProceedings { get; set; }
    public int? FoundCompletedBook { get; set; }
    public int? FoundOrganizedBook { get; set; }
    public int? FoundBookChapters { get; set; }
    public int? FoundBookTranslations { get; set; }
    public int? FoundParticipationEditorialCommittees { get; set; }
    #endregion

    #region (Resultados) Produção Artístca e Cultural - Produção Apresentada
    public int? FoundFullComposerSoloOrchestraAllTracks { get; set; }
    public int? FoundFullComposerSoloOrchestraCompilation { get; set; }
    public int? FoundChamberOrchestraInterpretation { get; set; }
    public int? FoundIndividualAndCollectiveArtPerformances { get; set; }
    public int? FoundScientificCulturalArtisticCollectionsCuratorship { get; set; }
    #endregion

    #region (Resultados) Produção Técnica - Produtos Registrados
    public int? FoundPatentLetter { get; set; }
    public int? FoundPatentDeposit { get; set; }
    public int? FoundSoftwareRegistration { get; set; }
    #endregion

    #region Critérios de Avaliação
    public int? APIndex { get; set; }
    public int? Qualification { get; set; }
    public int? ProjectProposalObjectives { get; set; }
    public int? AcademicScientificProductionCoherence { get; set; }
    public int? ProposalMethodologyAdaptation { get; set; }
    public int? EffectiveContributionToResearch { get; set; }
    #endregion
}