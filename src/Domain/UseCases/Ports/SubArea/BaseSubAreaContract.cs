using System.ComponentModel.DataAnnotations;

namespace Domain.UseCases.Ports.SubArea
{
    public abstract class BaseSubAreaContract
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}