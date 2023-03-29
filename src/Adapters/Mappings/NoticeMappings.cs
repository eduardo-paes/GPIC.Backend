using AutoMapper;
using Adapters.DTOs.Notice;
using Domain.Contracts.Notice;

namespace Adapters.Mappings
{
    public class NoticeMappings : Profile
    {
        public NoticeMappings()
        {
            CreateMap<CreateNoticeInput, CreateNoticeDTO>().ReverseMap();
            CreateMap<UpdateNoticeInput, UpdateNoticeDTO>().ReverseMap();
            CreateMap<ResumedReadNoticeOutput, ResumedReadNoticeDTO>().ReverseMap();
            CreateMap<DetailedReadNoticeOutput, DetailedReadNoticeDTO>().ReverseMap();
        }
    }
}