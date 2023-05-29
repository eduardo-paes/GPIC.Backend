using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Course
{
    public abstract class BaseCourseContract
    {
        [Required]
        public string? Name { get; set; }
    }
}