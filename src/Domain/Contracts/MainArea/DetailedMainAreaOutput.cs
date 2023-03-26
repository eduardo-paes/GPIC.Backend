namespace Domain.Contracts.MainArea
{
    public class DetailedMainAreaOutput : BaseMainAreaContract
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}