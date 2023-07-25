namespace Domain.Contracts.ProjectActivity
{
    public class ResumedReadProjectActivityOutput : BaseProjectActivityContract
    {
        public Guid? Id { get; set; }
        public int? InformedActivities { get; set; }
        public int? FoundActivities { get; set; }
    }
}