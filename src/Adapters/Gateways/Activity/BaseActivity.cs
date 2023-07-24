using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.Activity;
public class BaseActivity
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public double? Points { get; set; }
    public double? Limits { get; set; }
}