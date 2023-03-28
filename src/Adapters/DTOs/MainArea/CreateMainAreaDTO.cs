using Adapters.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.DTOs.MainArea
{
    public class CreateMainAreaDTO : RequestDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}