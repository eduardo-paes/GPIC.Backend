using System;
using System.ComponentModel.DataAnnotations;

namespace CopetSystem.Application.DTOs.Area
{
	public class CreateAreaDTO
	{
        [Required]
        public Guid? MainAreaId { get; set; }
        [Required]
        public string? Name { get; set; }
		[Required]
        public string? Code { get; set; }
    }
}

