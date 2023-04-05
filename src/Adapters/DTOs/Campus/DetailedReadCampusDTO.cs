using Adapters.DTOs.Base;

namespace Adapters.DTOs.Campus
{
    public class DetailedReadCampusDTO : ResponseDTO
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? Name { get; set; }
    }
}