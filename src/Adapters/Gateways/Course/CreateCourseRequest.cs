using System.ComponentModel.DataAnnotations;
using Adapters.Gateways.Base;

namespace Adapters.Gateways.Course
{
    public class CreateCourseRequest : Request
    {
        [Required]
        public string? Name { get; set; }
    }
}