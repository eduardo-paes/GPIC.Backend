using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Area
{
    public class BaseAreaContract
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}