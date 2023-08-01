using Adapters.Gateways.SubArea;
using AutoMapper;
using Domain.UseCases.Ports.SubArea;

namespace Adapters.Mappings
{
    public class SubAreaMappings : Profile
    {
        public SubAreaMappings()
        {
            _ = CreateMap<CreateSubAreaInput, CreateSubAreaRequest>().ReverseMap();
            _ = CreateMap<UpdateSubAreaInput, UpdateSubAreaRequest>().ReverseMap();
            _ = CreateMap<ResumedReadSubAreaOutput, ResumedReadSubAreaResponse>().ReverseMap();
            _ = CreateMap<DetailedReadSubAreaOutput, DetailedReadSubAreaResponse>()
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area))
                .ForPath(dest => dest.Area!.MainArea, opt => opt.MapFrom(src => src.Area!.MainArea))
                .ReverseMap();
        }
    }
}