using Adapters.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.DTOs.Course
{
    public class UpdateCourseDTO : RequestDTO
    {
        [Required]
        public string? Name { get; set; }
        public Guid? Id { get; set; }
    }
}