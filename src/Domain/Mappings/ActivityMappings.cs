using AutoMapper;
using Domain.Contracts.Activity;
using Domain.Entities;

namespace Domain.Mappings
{
    public class ActivityMappings : Profile
    {
        public ActivityMappings()
        {
            CreateMap<Activity, BaseActivity>();
            CreateMap<Activity, ActivityOutput>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DeletedAt, opt => opt.MapFrom(src => src.DeletedAt));

            CreateMap<ActivityType, BaseActivityType>();
            CreateMap<ActivityType, ActivityTypeOutput>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DeletedAt, opt => opt.MapFrom(src => src.DeletedAt))
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities));
        }
    }
}