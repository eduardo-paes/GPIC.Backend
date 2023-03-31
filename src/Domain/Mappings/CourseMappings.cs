using AutoMapper;
using Domain.Contracts.Course;
using Domain.Entities;

namespace Domain.Mappings
{
    public class CourseMappings : Profile
    {
        public CourseMappings()
        {
            CreateMap<Course, CreateCourseInput>().ReverseMap();
            CreateMap<Course, UpdateCourseInput>().ReverseMap();
            CreateMap<Course, ResumedReadCourseOutput>().ReverseMap();
            CreateMap<Course, DetailedReadCourseOutput>().ReverseMap();
        }
    }
}