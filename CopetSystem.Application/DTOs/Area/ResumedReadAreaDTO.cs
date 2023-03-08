using CopetSystem.Application.DTOs.MainArea;

namespace CopetSystem.Application.DTOs.Area
{
    public class ResumedReadAreaDTO : BaseAreaDTO
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}