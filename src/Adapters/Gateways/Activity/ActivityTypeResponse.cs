namespace Adapters.Gateways.Activity;
public class ActivityTypeResponse : BaseActivityType
{
    public Guid? Id { get; set; }
    public DateTime? DeletedAt { get; set; }
    new public IList<ActivityResponse>? Activities { get; set; }
}