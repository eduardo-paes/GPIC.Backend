using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Notice
{
    public abstract class BaseNoticeContract
    {
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? FinalDate { get; set; }
        [Required]
        public DateTime? AppealStartDate { get; set; }
        [Required]
        public DateTime? AppealFinalDate { get; set; }
        [Required]
        public int? SuspensionYears { get; set; }
        [Required]
        public int? SendingDocumentationDeadline { get; set; }

        public string? Description { get; set; }
        public string? DocUrl { get; set; }
    }
}