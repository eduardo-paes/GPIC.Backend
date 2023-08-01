using AutoMapper;
using Domain.Entities;
using Domain.Ports.Course;
using Domain.UseCases.Ports.Course;

namespace Domain.Mappings
{
    public class CourseMappings : Profile
    {
        public CourseMappings()
        {
            _ = CreateMap<Course, CreateCourseInput>().ReverseMap();
            _ = CreateMap<Course, UpdateCourseInput>().ReverseMap();
            _ = CreateMap<Course, ResumedReadCourseOutput>().ReverseMap();
            _ = CreateMap<Course, DetailedReadCourseOutput>().ReverseMap();
        }
    }
}