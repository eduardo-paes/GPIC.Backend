using System.ComponentModel.DataAnnotations;

namespace Domain.UseCases.Ports.Campus
{
    public abstract class BaseCampusContract
    {
        [Required]
        public string? Name { get; set; }
    }
}