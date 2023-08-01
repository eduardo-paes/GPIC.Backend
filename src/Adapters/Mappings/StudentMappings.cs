using Adapters.Gateways.Student;
using AutoMapper;
using Domain.UseCases.Ports.Student;

namespace Adapters.Mappings
{
    public class StudentMappings : Profile
    {
        public StudentMappings()
        {
            _ = CreateMap<CreateStudentInput, CreateStudentRequest>().ReverseMap();
            _ = CreateMap<UpdateStudentInput, UpdateStudentRequest>().ReverseMap();
            _ = CreateMap<ResumedReadStudentOutput, ResumedReadStudentResponse>().ReverseMap();
            _ = CreateMap<DetailedReadStudentOutput, DetailedReadStudentResponse>()
                .ForMember(dest => dest.User,
                    opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Campus,
                    opt => opt.MapFrom(src => src.Campus))
                .ForMember(dest => dest.Course,
                    opt => opt.MapFrom(src => src.Course))
                .ReverseMap();
        }
    }
}