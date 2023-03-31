using Adapters.DTOs.Base;

namespace Adapters.DTOs.Course
{
    public class ResumedReadCourseDTO : ResponseDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
    }
}