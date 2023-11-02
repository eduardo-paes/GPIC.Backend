namespace Application.Ports.Activity;
public class UpdateActivityInput : BaseActivity
{
    public Guid? Id { get; set; }
    public DateTime? DeletedAt { get; set; }
}