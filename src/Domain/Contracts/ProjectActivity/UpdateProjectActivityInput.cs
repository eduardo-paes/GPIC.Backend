namespace Domain.Contracts.ProjectActivity
{
    public class UpdateProjectActivityInput : BaseProjectActivityContract
    {
        public int? InformedActivities { get; set; }
        public int? FoundActivities { get; set; }
    }
}