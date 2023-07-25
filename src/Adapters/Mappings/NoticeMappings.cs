using AutoMapper;
using Adapters.Gateways.Notice;
using Domain.Contracts.Notice;

namespace Adapters.Mappings;
public class NoticeMappings : Profile
{
    public NoticeMappings()
    {
        CreateMap<CreateNoticeInput, CreateNoticeRequest>()
            .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
            .ReverseMap();
        CreateMap<UpdateNoticeInput, UpdateNoticeRequest>()
            .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
            .ReverseMap();
        CreateMap<ResumedReadNoticeOutput, ResumedReadNoticeResponse>().ReverseMap();
        CreateMap<DetailedReadNoticeOutput, DetailedReadNoticeResponse>().ReverseMap();
    }
}