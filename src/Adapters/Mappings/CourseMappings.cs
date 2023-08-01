using Adapters.Gateways.Course;
using AutoMapper;
using Domain.Ports.Course;
using Domain.UseCases.Ports.Course;

namespace Adapters.Mappings
{
    public class CourseMappings : Profile
    {
        public CourseMappings()
        {
            _ = CreateMap<CreateCourseInput, CreateCourseRequest>().ReverseMap();
            _ = CreateMap<UpdateCourseInput, UpdateCourseRequest>().ReverseMap();
            _ = CreateMap<ResumedReadCourseOutput, ResumedReadCourseResponse>().ReverseMap();
            _ = CreateMap<DetailedReadCourseOutput, DetailedReadCourseResponse>().ReverseMap();
        }
    }
}