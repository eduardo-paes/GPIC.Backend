using Domain.UseCases.Ports.Campus;
using Domain.UseCases.Ports.Course;
using Domain.UseCases.Ports.User;

namespace Domain.UseCases.Ports.Student
{
    public class DetailedReadStudentOutput : BaseStudentContract
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public UserReadOutput? User { get; set; }
        public DetailedReadCourseOutput? Course { get; set; }
        public DetailedReadCampusOutput? Campus { get; set; }
    }
}