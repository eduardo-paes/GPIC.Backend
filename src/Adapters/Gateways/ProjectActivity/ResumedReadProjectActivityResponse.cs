namespace Adapters.Gateways.ProjectActivity;
public class ResumedReadProjectActivityResponse : BaseProjectActivity
{
    public Guid? Id { get; set; }
    public Guid? ProjectId { get; set; }
    public int? FoundActivities { get; set; }
}