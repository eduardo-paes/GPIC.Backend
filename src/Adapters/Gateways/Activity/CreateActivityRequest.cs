using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.Activity;
public class CreateActivityRequest
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public double? Points { get; set; }
    [Required]
    public double? Limits { get; set; }
}