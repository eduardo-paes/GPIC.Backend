using Adapters.DTOs.MainArea;
using Adapters.DTOs.Base;

namespace Adapters.DTOs.Area
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