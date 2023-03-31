using System.ComponentModel.DataAnnotations;
using Adapters.DTOs.Base;

namespace Adapters.DTOs.Course
{
    public class CreateCourseDTO : RequestDTO
    {
        [Required]
        public string? Name { get; set; }
    }
}