using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.MainArea
{
    public class UpdateMainAreaInput : BaseMainAreaContract
    {
        public Guid? Id { get; set; }
    }
}

