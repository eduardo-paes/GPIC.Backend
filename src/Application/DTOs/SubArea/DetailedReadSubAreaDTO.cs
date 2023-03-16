using Application.DTOs.Area;

namespace Application.DTOs.SubArea
{
    public class DetailedReadSubAreaDTO : BaseSubAreaDTO
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual DetailedReadAreaDTO? Area { get; set; }
    }
}