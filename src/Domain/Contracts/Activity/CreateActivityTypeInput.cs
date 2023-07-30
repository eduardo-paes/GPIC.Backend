using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Activity;
public class CreateActivityTypeInput
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Unity { get; set; }
    [Required]
    public virtual IList<CreateActivityInput>? Activities { get; set; }
}