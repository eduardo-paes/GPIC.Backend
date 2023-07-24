using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.Activity;
public class UpdateActivityTypeRequest
{
    public Guid? Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Unity { get; set; }
    [Required]
    public virtual IList<UpdateActivityRequest>? Activities { get; set; }
}