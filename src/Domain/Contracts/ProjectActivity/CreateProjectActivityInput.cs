using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts.ProjectActivity
{
    public class CreateProjectActivityInput : BaseProjectActivityContract
    {
        [Required]
        public int? InformedActivities { get; set; }
    }
}