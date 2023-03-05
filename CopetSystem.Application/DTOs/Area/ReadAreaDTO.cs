using System;
using System.ComponentModel.DataAnnotations;

namespace CopetSystem.Application.DTOs.MainArea
{
	public class ReadMainAreaDTO
    {
		[Required]
        public Guid? Id { get; set; }
		[Required]
        public string? Name { get; set; }
		[Required]
        public string? Code { get; set; }
    }
}

