namespace Application.Ports.ProjectPartialReport
{
    public class UpdateProjectPartialReportInput
    {
        public int? CurrentDevelopmentStage { get; set; }
        public int? ScholarPerformance { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}