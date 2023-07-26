using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.ProjectEvaluation;
public class EvaluateAppealProjectInput
{
    [Required]
    public Guid? ProjectId { get; set; }
    [Required]
    public int? AppealEvaluationStatus { get; set; }
    [Required]
    public string? AppealEvaluationDescription { get; set; }
}