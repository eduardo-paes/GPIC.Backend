using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.Activity;
public class CreateActivityTypeRequest
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Unity { get; set; }
    [Required]
    public virtual IList<CreateActivityRequest>? Activities { get; set; }
}