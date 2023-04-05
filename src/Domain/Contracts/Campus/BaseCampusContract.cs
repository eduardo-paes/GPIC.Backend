using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Campus
{
    public class BaseCampusContract
    {
        [Required]
        public string? Name { get; set; }
    }
}