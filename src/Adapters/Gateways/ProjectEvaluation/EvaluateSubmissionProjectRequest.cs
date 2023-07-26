using System.ComponentModel.DataAnnotations;
using Adapters.Gateways.Base;
using Adapters.Gateways.ProjectActivity;

namespace Adapters.Gateways.ProjectEvaluation;
public class EvaluateSubmissionProjectRequest : IRequest
{
    #region Informações Gerais da Avaliação
    [Required]
    public Guid? ProjectId { get; set; }
    [Required]
    public bool IsProductivityFellow { get; set; }
    [Required]
    public int? SubmissionEvaluationStatus { get; set; }
    [Required]
    public string? SubmissionEvaluationDescription { get; set; }
    [Required]
    public IList<EvaluateProjectActivityRequest>? Activities { get; set; }
    #endregion

    #region Critérios de Avaliação
    [Required]
    public int? Qualification { get; set; }
    [Required]
    public int? ProjectProposalObjectives { get; set; }
    [Required]
    public int? AcademicScientificProductionCoherence { get; set; }
    [Required]
    public int? ProposalMethodologyAdaptation { get; set; }
    [Required]
    public int? EffectiveContributionToResearch { get; set; }
    #endregion
}