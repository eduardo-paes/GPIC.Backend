using Adapters.DTOs.Base;

namespace Adapters.DTOs.ProgramType
{
    public class ResumedReadProgramTypeDTO : ResponseDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}