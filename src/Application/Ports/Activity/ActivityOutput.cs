namespace Application.Ports.Activity;
public class ActivityOutput : BaseActivity
{
    public Guid? Id { get; set; }
    public DateTime? DeletedAt { get; set; }
}