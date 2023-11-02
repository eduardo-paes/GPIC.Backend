using System.ComponentModel.DataAnnotations;

namespace Application.Ports.ProjectPartialReport
{
    public abstract class BaseProjectPartialReportContract
    {
        [Required]
        public Guid? ProjectId { get; set; }
    }
}