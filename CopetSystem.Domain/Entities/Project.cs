namespace CopetSystem.Domain.Entities
{
    public class Project : Entity
    {
        public long ProgramTypeId { get; private set; }
        public long ProfessorId { get; private set; }
        public long StudentId { get; private set; }
        public long SubAreaId { get; private set; }
        public bool IsScholarshipCandidate { get; private set; }

        public string? Title { get; private set; }
        public string? KeyWord1 { get; private set; }
        public string? KeyWord2 { get; private set; }
        public string? KeyWord3 { get; private set; }
        public string? Objective { get; private set; }
        public string? Methodology { get; private set; }
        public string? ExpectedResults { get; private set; }
        public string? ActivitiesExecutionSchedule { get; private set; }
    }
}