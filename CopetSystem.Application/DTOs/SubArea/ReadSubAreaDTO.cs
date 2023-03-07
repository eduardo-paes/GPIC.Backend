using CopetSystem.Application.DTOs.Area;

namespace CopetSystem.Application.DTOs.SubArea
{
    public class ReadSubAreaDTO : BaseSubAreaDTO
    {
        public Guid? Id { get; set; }
        public virtual ReadAreaDTO? Area { get; set; }
    }
}

