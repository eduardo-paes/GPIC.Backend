using System.ComponentModel.DataAnnotations;

namespace Domain.UseCases.Ports.AssistanceType
{
    public abstract class BaseAssistanceTypeContract
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}