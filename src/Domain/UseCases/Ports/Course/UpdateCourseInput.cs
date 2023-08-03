namespace Domain.UseCases.Ports.Course
{
    public class UpdateCourseInput : BaseCourseContract
    {
        public Guid? Id { get; set; }
    }
}