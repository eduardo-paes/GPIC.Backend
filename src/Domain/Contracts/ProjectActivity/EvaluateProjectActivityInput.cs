using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.ProjectActivity;
public class EvaluateProjectActivityInput : BaseProjectActivityContract
{
    [Required]
    public Guid? ProjectId { get; set; }
    [Required]
    public int? FoundActivities { get; set; }
}