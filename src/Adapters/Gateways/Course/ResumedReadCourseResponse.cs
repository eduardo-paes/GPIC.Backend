using Adapters.Gateways.Base;

namespace Adapters.Gateways.Course
{
    public class ResumedReadCourseResponse : Response
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
    }
}