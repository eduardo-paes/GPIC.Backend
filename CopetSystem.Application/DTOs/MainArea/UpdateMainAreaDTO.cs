using System;
using System.ComponentModel.DataAnnotations;

namespace CopetSystem.Application.DTOs.MainArea
{
	public class UpdateMainAreaDTO : CreateMainAreaDTO
    {
        public Guid? Id { get; set; }
    }
}

