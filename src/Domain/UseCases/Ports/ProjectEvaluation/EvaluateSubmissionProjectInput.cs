using System.ComponentModel.DataAnnotations;
using Domain.UseCases.Ports.ProjectActivity;

namespace Domain.UseCases.Ports.ProjectEvaluation
{
    public class EvaluateSubmissionProjectInput
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
        public IList<EvaluateProjectActivityInput>? Activities { get; set; }
        #endregion Informações Gerais da Avaliação

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
        #endregion Critérios de Avaliação
    }
}