namespace Domain.UseCases.Ports.ProjectActivity
{
    public class ResumedReadProjectActivityOutput : BaseProjectActivityContract
    {
        public Guid? Id { get; set; }
        public Guid? ProjectId { get; set; }
        public int? FoundActivities { get; set; }
    }
}