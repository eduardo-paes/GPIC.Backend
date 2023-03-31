using AutoMapper;
using Adapters.DTOs.Course;
using Domain.Contracts.Course;

namespace Adapters.Mappings
{
    public class CourseMappings : Profile
    {
        public CourseMappings()
        {
            CreateMap<CreateCourseInput, CreateCourseDTO>().ReverseMap();
            CreateMap<UpdateCourseInput, UpdateCourseDTO>().ReverseMap();
            CreateMap<ResumedReadCourseOutput, ResumedReadCourseDTO>().ReverseMap();
            CreateMap<DetailedReadCourseOutput, DetailedReadCourseDTO>().ReverseMap();
        }
    }
}