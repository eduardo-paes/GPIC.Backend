using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.TypeAssistance
{
    public abstract class BaseTypeAssistanceContract
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}