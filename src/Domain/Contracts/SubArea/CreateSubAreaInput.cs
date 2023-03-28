using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.SubArea
{
    public class CreateSubAreaInput : BaseSubAreaContract
    {
        [Required]
        public Guid? AreaId { get; set; }
    }
}