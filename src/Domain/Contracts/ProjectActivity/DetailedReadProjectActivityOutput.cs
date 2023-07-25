namespace Domain.Contracts.ProjectActivity
{
    public class DetailedReadProjectActivityOutput : BaseProjectActivityContract
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? InformedActivities { get; set; }
        public int? FoundActivities { get; set; }
    }
}