using Application.DTOs.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Area
{
    public class CreateAreaDTO : RequestDTO
    {
        [Required]
        public Guid? MainAreaId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}

