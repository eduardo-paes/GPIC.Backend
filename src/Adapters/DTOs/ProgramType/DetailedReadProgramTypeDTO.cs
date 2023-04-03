using Adapters.DTOs.Base;

namespace Adapters.DTOs.ProgramType
{
    public class DetailedReadProgramTypeDTO : ResponseDTO
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}