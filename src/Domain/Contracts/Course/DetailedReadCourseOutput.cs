namespace Domain.Contracts.Course
{
    public class DetailedReadCourseOutput : BaseCourseContract
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}