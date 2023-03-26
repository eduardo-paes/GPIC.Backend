﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.MainArea
{
    public class BaseMainAreaContract
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}