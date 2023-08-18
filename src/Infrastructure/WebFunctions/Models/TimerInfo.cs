namespace WebFunctions.Models
{
    public class TimerInfo
    {
        public ScheduleStatus? ScheduleStatus { get; set; }
        public bool IsPastDue { get; set; }
    }
}