using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.MainArea
{
    public class ReadMainAreaDTO : BaseMainAreaDTO
    {
        public Guid? Id { get; set; }
    }
}

