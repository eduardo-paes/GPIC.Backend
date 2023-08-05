using System.ComponentModel.DataAnnotations;

namespace Application.Ports.Area
{
    public abstract class BaseAreaContract
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}