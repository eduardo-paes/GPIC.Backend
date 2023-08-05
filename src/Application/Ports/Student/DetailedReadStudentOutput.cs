using Application.Ports.Campus;
using Application.Ports.Course;
using Application.Ports.User;

namespace Application.Ports.Student
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