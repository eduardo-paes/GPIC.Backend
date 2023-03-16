using Application.DTOs.MainArea;

namespace Application.DTOs.Area
{
    public class DetailedReadAreaDTO : BaseAreaDTO
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual DetailedMainAreaDTO? MainArea { get; set; }
    }
}