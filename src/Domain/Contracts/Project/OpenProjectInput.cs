namespace Domain.Contracts.Project
{
    public class OpenProjectInput
    {
        #region Informações do Projeto
        public string? Title { get; set; }
        public string? KeyWord1 { get; set; }
        public string? KeyWord2 { get; set; }
        public string? KeyWord3 { get; set; }
        public bool IsScholarshipCandidate { get; set; }
        public string? Objective { get; set; }
        public string? Methodology { get; set; }
        public string? ExpectedResults { get; set; }
        public string? ActivitiesExecutionSchedule { get; set; }
        #endregion

        #region Relacionamentos
        public Guid? ProgramTypeId { get; set; }
        public Guid? ProfessorId { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? SubAreaId { get; set; }
        public Guid? NoticeId { get; set; }
        #endregion
    }
}