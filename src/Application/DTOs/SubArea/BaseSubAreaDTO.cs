using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.SubArea
{
    public class BaseSubAreaDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}