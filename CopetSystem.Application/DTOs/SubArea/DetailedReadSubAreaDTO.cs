using CopetSystem.Application.DTOs.Area;

namespace CopetSystem.Application.DTOs.SubArea
{
    public class DetailedReadSubAreaDTO : BaseSubAreaDTO
    {
        public Guid? Id { get; set; }
        public virtual DetailedReadAreaDTO? Area { get; set; }
    }
}