using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.ProjectActivity;
public abstract class BaseProjectActivity
{
    [Required]
    public Guid? ActivityId { get; set; }
    [Required]
    public int? InformedActivities { get; set; }
}