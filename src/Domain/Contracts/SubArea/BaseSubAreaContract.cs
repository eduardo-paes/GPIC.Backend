using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.SubArea
{
    public class BaseSubAreaContract
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}