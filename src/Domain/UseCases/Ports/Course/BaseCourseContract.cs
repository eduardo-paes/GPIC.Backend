using System.ComponentModel.DataAnnotations;

namespace Domain.UseCases.Ports.Course
{
    public abstract class BaseCourseContract
    {
        [Required]
        public string? Name { get; set; }
    }
}