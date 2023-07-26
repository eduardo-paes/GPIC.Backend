using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.ProjectActivity;
public class EvaluateProjectActivityRequest : BaseProjectActivity
{
    [Required]
    public Guid? ProjectId { get; set; }
    [Required]
    public int? FoundActivities { get; set; }
}