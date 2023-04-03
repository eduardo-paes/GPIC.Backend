using Adapters.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.DTOs.ProgramType
{
    public class UpdateProgramTypeDTO : RequestDTO
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? Id { get; set; }
    }
}