using Adapters.DTOs.Base;

namespace Adapters.DTOs.Notice
{
    public class ResumedReadNoticeDTO : ResponseDTO
    {
        public Guid? Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public string? Description { get; set; }
        public string? DocUrl { get; set; }
    }
}