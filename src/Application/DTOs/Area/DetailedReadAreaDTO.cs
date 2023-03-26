using Application.DTOs.MainArea;
using Application.DTOs.Base;

namespace Application.DTOs.Area
{
    public class DetailedReadAreaDTO : ResponseDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual DetailedMainAreaDTO? MainArea { get; set; }
    }
}