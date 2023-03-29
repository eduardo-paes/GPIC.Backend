using System.ComponentModel.DataAnnotations;
using Adapters.DTOs.Base;
using Microsoft.AspNetCore.Http;

namespace Adapters.DTOs.Notice
{
    public class CreateNoticeDTO : RequestDTO
    {
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? FinalDate { get; set; }

        public string? Description { get; set; }
        public IFormFile? File { get; set; }
    }
}