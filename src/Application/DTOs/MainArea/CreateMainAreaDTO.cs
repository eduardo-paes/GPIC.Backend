using Application.DTOs.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.MainArea
{
    public class CreateMainAreaDTO : RequestDTO
    { 
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}

