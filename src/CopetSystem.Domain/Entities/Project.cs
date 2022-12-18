namespace CopetSystem.Domain.Entities
{
    public class Project : Entity
    {
        public long ProgramTypeId { get; set; }
        public long ProfessorId { get; set; }
        public long StudentId { get; set; }
        public long SubAreaId { get; set; }
        public bool IsScholarshipCandidate { get; set; }

        public string? Title { get; set; }
        public string? KeyWord1 { get; set; }
        public string? KeyWord2 { get; set; }
        public string? KeyWord3 { get; set; }
        public string? Objective { get; set; }
        public string? Methodology { get; set; }
        public string? ExpectedResults { get; set; }
        public string? ActivitiesExecutionSchedule { get; set; }
    }
}