using System.ComponentModel.DataAnnotations;

namespace Application.Ports.Campus
{
    public abstract class BaseCampusContract
    {
        [Required]
        public string? Name { get; set; }
    }
}