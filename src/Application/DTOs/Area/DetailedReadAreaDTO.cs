using Application.DTOs.MainArea;

namespace Application.DTOs.Area
{
    public class DetailedReadAreaDTO : BaseAreaDTO
    {
        public Guid? Id { get; set; }
        public virtual ReadMainAreaDTO? MainArea { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}