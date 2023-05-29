using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.ProgramType
{
    public abstract class BaseProgramTypeContract
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}