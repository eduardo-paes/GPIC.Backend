using AutoMapper;
using Domain.Entities;
using Domain.UseCases.Ports.Notice;

namespace Domain.Mappings
{
    public class NoticeMappings : Profile
    {
        public NoticeMappings()
        {
            _ = CreateMap<Notice, CreateNoticeInput>().ReverseMap();
            _ = CreateMap<Notice, UpdateNoticeInput>().ReverseMap();
            _ = CreateMap<Notice, ResumedReadNoticeOutput>().ReverseMap();
            _ = CreateMap<Notice, DetailedReadNoticeOutput>().ReverseMap();
        }
    }
}