using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Activity;
public class BaseActivityType
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Unity { get; set; }
    [Required]
    public virtual IList<BaseActivity>? Activities { get; set; }
}