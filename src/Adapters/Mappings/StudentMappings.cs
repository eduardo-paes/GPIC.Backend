using AutoMapper;
using Adapters.Gateways.Student;
using Domain.Contracts.Student;

namespace Adapters.Mappings
{
    public class StudentMappings : Profile
    {
        public StudentMappings()
        {
            CreateMap<DetailedReadStudentOutput, DetailedReadStudentResponse>()
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