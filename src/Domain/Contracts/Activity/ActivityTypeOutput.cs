namespace Domain.Contracts.Activity;
public class ActivityTypeOutput : BaseActivityType
{
    public Guid? Id { get; set; }
    public DateTime? DeletedAt { get; set; }
    new public IList<ActivityOutput>? Activities { get; set; }
}