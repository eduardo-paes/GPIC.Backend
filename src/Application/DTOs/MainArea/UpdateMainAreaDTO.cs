using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.MainArea
{
    public class UpdateMainAreaDTO : BaseMainAreaDTO
    {
        public Guid? Id { get; set; }
    }
}

