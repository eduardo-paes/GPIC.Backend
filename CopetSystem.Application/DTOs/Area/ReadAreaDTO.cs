using System;
using System.ComponentModel.DataAnnotations;
using CopetSystem.Application.DTOs.MainArea;

namespace CopetSystem.Application.DTOs.Area
{
	public class ReadAreaDTO : BaseAreaDTO
    {
        public Guid? Id { get; set; }
        public virtual ReadMainAreaDTO? MainArea { get; set; }
    }
}

