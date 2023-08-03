using Adapters.Gateways.Notice;
using AutoMapper;
using Domain.UseCases.Ports.Notice;

namespace Adapters.Mappings
{
    public class NoticeMappings : Profile
    {
        public NoticeMappings()
        {
            _ = CreateMap<CreateNoticeInput, CreateNoticeRequest>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
                .ReverseMap();
            _ = CreateMap<UpdateNoticeInput, UpdateNoticeRequest>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
                .ReverseMap();
            _ = CreateMap<ResumedReadNoticeOutput, ResumedReadNoticeResponse>().ReverseMap();
            _ = CreateMap<DetailedReadNoticeOutput, DetailedReadNoticeResponse>().ReverseMap();
        }
    }
}