namespace Domain.UseCases.Ports.ProjectReport
{
    public class DetailedReadProjectReportOutput : BaseProjectReportContract
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? SendDate { get; set; }
        public string? ReportUrl { get; set; }
        public Guid? UserId { get; set; }
    }
}