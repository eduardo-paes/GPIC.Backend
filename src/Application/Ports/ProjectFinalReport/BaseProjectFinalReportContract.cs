using System.ComponentModel.DataAnnotations;

namespace Application.Ports.ProjectFinalReport
{
    public abstract class BaseProjectFinalReportContract
    {
        [Required]
        public int? ReportType { get; set; }
        [Required]
        public Guid? ProjectId { get; set; }
    }
}