using Application.DTOs.MainArea;

namespace Application.DTOs.Area
{
    public class ResumedReadAreaDTO : BaseAreaDTO
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}