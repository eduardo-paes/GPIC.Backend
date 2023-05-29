using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Campus
{
    public abstract class BaseCampusContract
    {
        [Required]
        public string? Name { get; set; }
    }
}