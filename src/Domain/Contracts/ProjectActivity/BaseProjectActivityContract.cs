using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.ProjectActivity;
public abstract class BaseProjectActivityContract
{
    [Required]
    public Guid? ActivityId { get; set; }
    [Required]
    public int? InformedActivities { get; set; }
}