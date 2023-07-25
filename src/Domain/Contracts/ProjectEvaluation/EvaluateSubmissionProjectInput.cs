namespace Domain.Contracts.ProjectEvaluation;
public class EvaluateSubmissionProjectInput
{
    #region Informações Gerais da Avaliação
    public Guid? ProjectId { get; set; }
    public bool IsProductivityFellow { get; set; }
    public int? SubmissionEvaluationStatus { get; set; }
    public string? SubmissionEvaluationDescription { get; set; }
    #endregion

    #region Critérios de Avaliação
    public int? Qualification { get; set; }
    public int? ProjectProposalObjectives { get; set; }
    public int? AcademicScientificProductionCoherence { get; set; }
    public int? ProposalMethodologyAdaptation { get; set; }
    public int? EffectiveContributionToResearch { get; set; }
    #endregion
}