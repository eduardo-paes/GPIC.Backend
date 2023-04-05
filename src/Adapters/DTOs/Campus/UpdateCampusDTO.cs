using Adapters.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.DTOs.Campus
{
    public class UpdateCampusDTO : RequestDTO
    {
        [Required]
        public string? Name { get; set; }
        public Guid? Id { get; set; }
    }
}