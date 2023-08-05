using System.ComponentModel.DataAnnotations;

namespace Application.Ports.Course
{
    public abstract class BaseCourseContract
    {
        [Required]
        public string? Name { get; set; }
    }
}