using System.ComponentModel.DataAnnotations;

namespace Application.Ports.ProjectPartialReport
{
    public class CreateProjectPartialReportInput : BaseProjectPartialReportContract
    {
        [Required]
        public int CurrentDevelopmentStage { get; set; }

        [Required]
        public int ScholarPerformance { get; set; }

        public string? AdditionalInfo { get; set; }
    }
}