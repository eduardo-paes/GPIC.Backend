namespace Adapters.Gateways.Activity;
public class ActivityResponse : BaseActivity
{
    public Guid? Id { get; set; }
    public DateTime? DeletedAt { get; set; }
}