using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Notice
{
    public class BaseNoticeContract
    {
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? FinalDate { get; set; }

        public string? Description { get; set; }
        public string? DocUrl { get; set; }
    }
}