using System.ComponentModel.DataAnnotations;
using Adapters.DTOs.Base;

namespace Adapters.DTOs.Campus
{
    public class CreateCampusDTO : RequestDTO
    {
        [Required]
        public string? Name { get; set; }
    }
}