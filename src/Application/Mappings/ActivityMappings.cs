using AutoMapper;
using Domain.Entities;
using Application.Ports.Activity;

namespace Domain.Mappings
{
    public class ActivityMappings : Profile
    {
        public ActivityMappings()
        {
            _ = CreateMap<Activity, BaseActivity>();
            _ = CreateMap<Activity, ActivityOutput>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DeletedAt, opt => opt.MapFrom(src => src.DeletedAt));

            _ = CreateMap<ActivityType, BaseActivityType>();
            _ = CreateMap<ActivityType, ActivityTypeOutput>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DeletedAt, opt => opt.MapFrom(src => src.DeletedAt))
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities));
        }
    }
}