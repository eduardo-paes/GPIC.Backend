using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Contracts.MainArea
{
    public class ResumedReadMainAreaOutput : BaseMainAreaContract
    {
        public Guid? Id { get; set; }
    }
}