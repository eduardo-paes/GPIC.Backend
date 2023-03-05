using System;
using System.ComponentModel.DataAnnotations;

namespace CopetSystem.Application.DTOs.Area
{
	public class UpdateAreaDTO : BaseAreaDTO
    {
        public Guid? Id { get; set; }
    }
}

