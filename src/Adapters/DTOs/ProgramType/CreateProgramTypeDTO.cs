using System.ComponentModel.DataAnnotations;
using Adapters.DTOs.Base;

namespace Adapters.DTOs.ProgramType
{
    public class CreateProgramTypeDTO : RequestDTO
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}