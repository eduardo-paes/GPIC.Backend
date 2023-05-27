using System.ComponentModel.DataAnnotations;
using Adapters.Gateways.Base;

namespace Adapters.Gateways.Notice
{
    public class DetailedReadNoticeResponse : Response
    {
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? FinalDate { get; set; }

        public string? Description { get; set; }
        public string? DocUrl { get; set; }
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}