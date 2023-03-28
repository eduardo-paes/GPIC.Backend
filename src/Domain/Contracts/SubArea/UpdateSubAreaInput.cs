using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.SubArea
{
    public class UpdateSubAreaInput : BaseSubAreaContract
    {
        [Required]
        public Guid? AreaId { get; set; }
        public Guid? Id { get; set; }
    }
}