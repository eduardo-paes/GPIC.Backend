using System.ComponentModel.DataAnnotations;

namespace Domain.UseCases.Ports.MainArea
{
    public abstract class BaseMainAreaContract
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}