using System;
using System.ComponentModel.DataAnnotations;

namespace CopetSystem.Application.DTOs.Area
{
	public class CreateAreaDTO : BaseAreaDTO
	{
        [Required]
        public Guid? MainAreaId { get; set; }
    }
}

