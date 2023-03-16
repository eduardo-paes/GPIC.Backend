using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Area
{
    public class BaseAreaDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}