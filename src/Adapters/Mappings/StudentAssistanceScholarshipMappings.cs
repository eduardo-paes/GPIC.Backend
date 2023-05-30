using AutoMapper;
using Adapters.Gateways.StudentAssistanceScholarship;
using Domain.Contracts.StudentAssistanceScholarship;

namespace Adapters.Mappings
{
    public class StudentAssistanceScholarshipMappings : Profile
    {
        public StudentAssistanceScholarshipMappings()
        {
            CreateMap<CreateStudentAssistanceScholarshipInput, CreateStudentAssistanceScholarshipRequest>().ReverseMap();
            CreateMap<UpdateStudentAssistanceScholarshipInput, UpdateStudentAssistanceScholarshipRequest>().ReverseMap();
            CreateMap<ResumedReadStudentAssistanceScholarshipOutput, ResumedReadStudentAssistanceScholarshipResponse>().ReverseMap();
            CreateMap<DetailedReadStudentAssistanceScholarshipOutput, DetailedReadStudentAssistanceScholarshipResponse>().ReverseMap();
        }
    }
}