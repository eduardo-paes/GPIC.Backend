using System.ComponentModel.DataAnnotations;
using Adapters.DTOs.Base;

namespace Adapters.DTOs.Notice
{
    public class DetailedReadNoticeDTO : ResponseDTO
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