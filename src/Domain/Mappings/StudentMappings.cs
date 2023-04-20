using AutoMapper;
using Domain.Contracts.Student;
using Domain.Entities;

namespace Domain.Mappings
{
    public class StudentMappings : Profile
    {
        public StudentMappings()
        {
            CreateMap<Student, CreateStudentInput>().ReverseMap();
            CreateMap<Student, UpdateStudentInput>().ReverseMap();

            CreateMap<Student, ResumedReadStudentOutput>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.User != null ? src.User.Name : null))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.User != null ? src.User.Email : null))
                .ReverseMap();

            CreateMap<Student, DetailedReadStudentOutput>()
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