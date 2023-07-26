using System.ComponentModel.DataAnnotations;
using Adapters.Gateways.Base;
using Adapters.Gateways.ProjectActivity;

namespace Adapters.Gateways.Project;
public class OpenProjectRequest : IRequest
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
    public virtual IList<CreateProjectActivityRequest>? Activities { get; set; }
    #endregion

    #region Relacionamentos
    [Required]
    public Guid? ProgramTypeId { get; set; }
    [Required]
    public Guid? ProfessorId { get; set; }
    [Required]
    public Guid? SubAreaId { get; set; }
    [Required]
    public Guid? NoticeId { get; set; }
    public Guid? StudentId { get; set; }
    #endregion
}