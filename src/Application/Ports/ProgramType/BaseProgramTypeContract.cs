using System.ComponentModel.DataAnnotations;

namespace Application.Ports.ProgramType
{
    public abstract class BaseProgramTypeContract
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}