using System.ComponentModel.DataAnnotations;

namespace Application.Ports.Notice
{
    public abstract class BaseNoticeContract
    {
        [Required]
        public DateTime? RegistrationStartDate { get; set; }
        [Required]
        public DateTime? RegistrationEndDate { get; set; }
        [Required]
        public DateTime? EvaluationStartDate { get; set; }
        [Required]
        public DateTime? EvaluationEndDate { get; set; }
        [Required]
        public DateTime? AppealStartDate { get; set; }
        [Required]
        public DateTime? AppealEndDate { get; set; }
        [Required]
        public DateTime? SendingDocsStartDate { get; set; }
        [Required]
        public DateTime? SendingDocsEndDate { get; set; }
        [Required]
        public DateTime? PartialReportDeadline { get; set; }
        [Required]
        public DateTime? FinalReportDeadline { get; set; }
        [Required]
        public int? SuspensionYears { get; set; }

        public string? Description { get; set; }
    }
}