using System.ComponentModel.DataAnnotations;

namespace Application.Ports.Activity
{
    public class UpdateActivityTypeInput
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Unity { get; set; }
        [Required]
        public IList<UpdateActivityInput>? Activities { get; set; }

        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}