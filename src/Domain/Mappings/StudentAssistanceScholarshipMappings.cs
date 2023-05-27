using AutoMapper;
using Domain.Contracts.StudentAssistanceScholarship;
using Domain.Entities;

namespace Domain.Mappings
{
    public class StudentAssistanceScholarshipMappings : Profile
    {
        public StudentAssistanceScholarshipMappings()
        {
            CreateMap<StudentAssistanceScholarship, CreateStudentAssistanceScholarshipInput>().ReverseMap();
            CreateMap<StudentAssistanceScholarship, UpdateStudentAssistanceScholarshipInput>().ReverseMap();
            CreateMap<StudentAssistanceScholarship, ResumedReadStudentAssistanceScholarshipOutput>().ReverseMap();
            CreateMap<StudentAssistanceScholarship, DetailedReadStudentAssistanceScholarshipOutput>().ReverseMap();
        }
    }
}