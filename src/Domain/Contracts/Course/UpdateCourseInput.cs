namespace Domain.Contracts.Course
{
    public class UpdateCourseInput : BaseCourseContract
    {
        public Guid? Id { get; set; }
    }
}