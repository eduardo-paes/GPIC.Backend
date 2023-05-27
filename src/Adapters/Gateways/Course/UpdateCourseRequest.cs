using Adapters.Gateways.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.Course
{
    public class UpdateCourseRequest : Request
    {
        [Required]
        public string? Name { get; set; }
        public Guid? Id { get; set; }
    }
}