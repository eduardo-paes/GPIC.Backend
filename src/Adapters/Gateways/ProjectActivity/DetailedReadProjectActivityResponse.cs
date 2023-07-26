namespace Adapters.Gateways.ProjectActivity;
public class DetailedReadProjectActivityResponse : BaseProjectActivity
{
    public Guid? Id { get; set; }
    public Guid? ProjectId { get; set; }
    public int? FoundActivities { get; set; }
    public DateTime? DeletedAt { get; set; }
}