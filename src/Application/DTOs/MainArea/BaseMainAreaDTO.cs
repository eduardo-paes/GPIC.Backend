using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.MainArea
{
    public class BaseMainAreaDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}