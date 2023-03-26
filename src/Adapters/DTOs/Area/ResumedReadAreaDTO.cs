using Adapters.DTOs.Base;

namespace Adapters.DTOs.Area
{
    public class ResumedReadAreaDTO : ResponseDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}