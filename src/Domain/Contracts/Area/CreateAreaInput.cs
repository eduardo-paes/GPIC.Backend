using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.Area
{
    public class CreateAreaInput : BaseAreaContract
    {
        [Required]
        public Guid? MainAreaId { get; set; }
    }
}