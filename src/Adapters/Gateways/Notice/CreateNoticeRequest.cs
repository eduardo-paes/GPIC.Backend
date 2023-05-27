using System.ComponentModel.DataAnnotations;
using Adapters.Gateways.Base;
using Microsoft.AspNetCore.Http;

namespace Adapters.Gateways.Notice
{
    public class CreateNoticeRequest : Request
    {
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? FinalDate { get; set; }

        public string? Description { get; set; }
        public IFormFile? File { get; set; }
    }
}