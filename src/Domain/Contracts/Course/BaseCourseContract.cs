using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Course
{
    public class BaseCourseContract
    {
        [Required]
        public string? Name { get; set; }
    }
}