using System;
using System.ComponentModel.DataAnnotations;

namespace CopetSystem.Application.DTOs.MainArea
{
	public class CreateMainAreaDTO
	{
		[Required]
        public string? Name { get; set; }
		[Required]
        public string? Code { get; set; }
    }
}

