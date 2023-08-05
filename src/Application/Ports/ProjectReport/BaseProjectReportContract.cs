using System.ComponentModel.DataAnnotations;

namespace Application.Ports.ProjectReport
{
    public abstract class BaseProjectReportContract
    {
        [Required]
        public int? ReportType { get; set; }
        [Required]
        public Guid? ProjectId { get; set; }
    }
}