using System.ComponentModel.DataAnnotations;

namespace Application.Ports.SubArea
{
    public abstract class BaseSubAreaContract
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}