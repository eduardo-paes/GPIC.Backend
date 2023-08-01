namespace Domain.UseCases.Ports.Notice
{
    public class ResumedReadNoticeOutput : BaseNoticeContract
    {
        public Guid? Id { get; set; }
        public string? DocUrl { get; set; }
    }
}
