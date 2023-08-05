namespace Application.Ports.ProjectEvaluation
{
    public class DetailedReadProjectEvaluationOutput
    {
        #region Informações Gerais da Avaliação
        public Guid? ProjectId { get; set; }
        public bool IsProductivityFellow { get; set; }
        public Guid? SubmissionEvaluatorId { get; set; }
        public int? SubmissionEvaluationStatus { get; set; }
        public DateTime? SubmissionEvaluationDate { get; set; }
        public string? SubmissionEvaluationDescription { get; set; }
        public Guid? AppealEvaluatorId { get; set; }
        public int? AppealEvaluationStatus { get; set; }
        public DateTime? AppealEvaluationDate { get; set; }
        public string? AppealEvaluationDescription { get; set; }
        #endregion Informações Gerais da Avaliação

        #region Critérios de Avaliação
        public int? APIndex { get; set; }
        public int? Qualification { get; set; }
        public int? ProjectProposalObjectives { get; set; }
        public int? AcademicScientificProductionCoherence { get; set; }
        public int? ProposalMethodologyAdaptation { get; set; }
        public int? EffectiveContributionToResearch { get; set; }
        #endregion Critérios de Avaliação
    }
}