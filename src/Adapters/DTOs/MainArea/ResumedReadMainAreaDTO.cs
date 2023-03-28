using Adapters.DTOs.Base;

namespace Adapters.DTOs.MainArea
{
    public class ResumedReadMainAreaDTO : ResponseDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}