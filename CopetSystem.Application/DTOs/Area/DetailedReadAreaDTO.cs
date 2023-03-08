using CopetSystem.Application.DTOs.MainArea;

namespace CopetSystem.Application.DTOs.Area
{
    public class DetailedReadAreaDTO : BaseAreaDTO
    {
        public Guid? Id { get; set; }
        public virtual ReadMainAreaDTO? MainArea { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}