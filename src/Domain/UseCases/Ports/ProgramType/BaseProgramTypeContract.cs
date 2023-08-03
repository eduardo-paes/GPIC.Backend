using System.ComponentModel.DataAnnotations;

namespace Domain.UseCases.Ports.ProgramType
{
    public abstract class BaseProgramTypeContract
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}