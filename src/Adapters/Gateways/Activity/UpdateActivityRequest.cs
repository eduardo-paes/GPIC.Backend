using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.Activity;
public class UpdateActivityRequest
{
    public Guid? Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public double? Points { get; set; }
    [Required]
    public double? Limits { get; set; }
}