namespace Domain.Contracts.ProjectEvaluation;
public class EvaluateAppealProjectInput
{
    public Guid? ProjectId { get; set; }
    public int? AppealEvaluationStatus { get; set; }
    public string? AppealEvaluationDescription { get; set; }
}