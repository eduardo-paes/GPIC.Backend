using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Activity;
public class BaseActivity
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public double? Points { get; set; }
    public double? Limits { get; set; }
}