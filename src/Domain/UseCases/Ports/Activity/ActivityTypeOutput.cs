using Domain.Ports.Activity;

namespace Domain.UseCases.Ports.Activity
{
    public class ActivityTypeOutput : BaseActivityType
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public new IList<ActivityOutput>? Activities { get; set; }
    }
}