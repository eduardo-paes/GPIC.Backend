using Adapters.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adapters.DTOs.MainArea
{
    public class ResumedReadMainAreaDTO : ResponseDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}