using AutoMapper;
using Adapters.Gateways.Course;
using Domain.Contracts.Course;

namespace Adapters.Mappings
{
    public class CourseMappings : Profile
    {
        public CourseMappings()
        {
            CreateMap<CreateCourseInput, CreateCourseRequest>().ReverseMap();
            CreateMap<UpdateCourseInput, UpdateCourseRequest>().ReverseMap();
            CreateMap<ResumedReadCourseOutput, ResumedReadCourseResponse>().ReverseMap();
            CreateMap<DetailedReadCourseOutput, DetailedReadCourseResponse>().ReverseMap();
        }
    }
}