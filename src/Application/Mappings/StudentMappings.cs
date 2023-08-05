using AutoMapper;
using Domain.Entities;
using Application.Ports.Student;

namespace Domain.Mappings
{
    public class StudentMappings : Profile
    {
        public StudentMappings()
        {
            _ = CreateMap<Student, CreateStudentInput>().ReverseMap();
            _ = CreateMap<Student, UpdateStudentInput>().ReverseMap();

            _ = CreateMap<Student, ResumedReadStudentOutput>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.User != null ? src.User.Name : null))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.User != null ? src.User.Email : null))
                .ReverseMap();

            _ = CreateMap<Student, DetailedReadStudentOutput>()
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