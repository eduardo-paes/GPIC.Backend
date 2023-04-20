using Domain.Contracts.Campus;
using Domain.Contracts.Course;
using Domain.Contracts.User;

namespace Domain.Contracts.Student
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