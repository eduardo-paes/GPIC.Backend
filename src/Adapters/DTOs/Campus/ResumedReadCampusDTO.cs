using Adapters.DTOs.Base;

namespace Adapters.DTOs.Campus
{
    public class ResumedReadCampusDTO : ResponseDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
    }
}