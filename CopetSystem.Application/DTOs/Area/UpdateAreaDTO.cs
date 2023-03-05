using System;
using System.ComponentModel.DataAnnotations;

namespace CopetSystem.Application.DTOs.Area
{
	public class UpdateAreaDTO : BaseAreaDTO
    {
        [Required]
        public Guid? MainAreaId { get; set; }
        public Guid? Id { get; set; }
    }
}

