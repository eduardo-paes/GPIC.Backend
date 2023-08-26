namespace Application.Ports.ProjectPartialReport
{
    public class DetailedReadProjectPartialReportOutput : BaseProjectPartialReportContract
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}