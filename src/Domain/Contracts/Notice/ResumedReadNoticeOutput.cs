namespace Domain.Contracts.Notice
{
    public class ResumedReadNoticeOutput : BaseNoticeContract
    {
        public Guid? Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinalDate { get; set; }

        public string? Description { get; set; }
        public string? DocUrl { get; set; }
    }
}
