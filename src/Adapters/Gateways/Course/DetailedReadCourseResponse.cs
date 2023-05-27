using Adapters.Gateways.Base;

namespace Adapters.Gateways.Course
{
    public class DetailedReadCourseResponse : Response
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? Name { get; set; }
    }
}