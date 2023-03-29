using AutoMapper;
using Domain.Contracts.Notice;
using Domain.Entities;

namespace Domain.Mappings
{
    public class NoticeMappings : Profile
    {
        public NoticeMappings()
        {
            CreateMap<Notice, CreateNoticeInput>().ReverseMap();
            CreateMap<Notice, UpdateNoticeInput>().ReverseMap();
            CreateMap<Notice, ResumedReadNoticeOutput>().ReverseMap();
            CreateMap<Notice, DetailedReadNoticeOutput>().ReverseMap();
        }
    }
}