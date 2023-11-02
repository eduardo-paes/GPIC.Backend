namespace Application.Ports.ProjectActivity
{
    public class DetailedReadProjectActivityOutput : BaseProjectActivityContract
    {
        public Guid? Id { get; set; }
        public Guid? ProjectId { get; set; }
        public int? FoundActivities { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}