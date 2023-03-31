using Adapters.DTOs.Base;

namespace Adapters.DTOs.Course
{
    public class DetailedReadCourseDTO : ResponseDTO
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? Name { get; set; }
    }
}