using System.ComponentModel.DataAnnotations;
using Domain.UseCases.Ports.ProjectActivity;

namespace Domain.UseCases.Ports.Project
{
    public class UpdateProjectInput
    {
        #region Informações do Projeto
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? KeyWord1 { get; set; }
        [Required]
        public string? KeyWord2 { get; set; }
        [Required]
        public string? KeyWord3 { get; set; }
        [Required]
        public bool IsScholarshipCandidate { get; set; }
        [Required]
        public string? Objective { get; set; }
        [Required]
        public string? Methodology { get; set; }
        [Required]
        public string? ExpectedResults { get; set; }
        [Required]
        public string? ActivitiesExecutionSchedule { get; set; }
        [Required]
        public IList<UpdateProjectActivityInput>? Activities { get; set; }
        #endregion Informações do Projeto

        #region Relacionamentos
        [Required]
        public Guid? ProgramTypeId { get; set; }
        [Required]
        public Guid? SubAreaId { get; set; }
        public Guid? StudentId { get; set; }
        #endregion Relacionamentos
    }
}