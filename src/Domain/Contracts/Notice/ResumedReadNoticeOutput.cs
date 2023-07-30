namespace Domain.Contracts.Notice
{
    public class ResumedReadNoticeOutput : BaseNoticeContract
    {
        public Guid? Id { get; set; }
        public string? DocUrl { get; set; }
    }
}
