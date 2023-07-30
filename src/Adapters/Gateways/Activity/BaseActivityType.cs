using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.Activity;
public class BaseActivityType
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Unity { get; set; }
    [Required]
    public virtual IList<BaseActivity>? Activities { get; set; }
}