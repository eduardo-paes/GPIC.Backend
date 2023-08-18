namespace WebFunctions.Models
{
    public class CustomTimerInfo
    {
        public CustomScheduleStatus? ScheduleStatus { get; set; }
        public bool IsPastDue { get; set; }
    }
}