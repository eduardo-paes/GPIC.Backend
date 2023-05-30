using AutoMapper;
using Adapters.Gateways.Notice;
using Domain.Contracts.Notice;

namespace Adapters.Mappings
{
    public class NoticeMappings : Profile
    {
        public NoticeMappings()
        {
            CreateMap<CreateNoticeInput, CreateNoticeRequest>().ReverseMap();
            CreateMap<UpdateNoticeInput, UpdateNoticeRequest>().ReverseMap();
            CreateMap<ResumedReadNoticeOutput, ResumedReadNoticeResponse>().ReverseMap();
            CreateMap<DetailedReadNoticeOutput, DetailedReadNoticeResponse>().ReverseMap();
        }
    }
}