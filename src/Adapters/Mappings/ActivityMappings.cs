using Adapters.Gateways.Activity;
using AutoMapper;
using Domain.Contracts.Activity;

namespace Adapters.Mappings
{
    public class ActivityMappings : Profile
    {
        public ActivityMappings()
        {
            CreateMap<CreateActivityInput, CreateActivityRequest>().ReverseMap();
            CreateMap<CreateActivityTypeInput, CreateActivityTypeRequest>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
                .ReverseMap();
            CreateMap<UpdateActivityInput, UpdateActivityRequest>().ReverseMap();
            CreateMap<UpdateActivityTypeInput, UpdateActivityTypeRequest>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
                .ForPath(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
                .ReverseMap();
            CreateMap<ActivityOutput, ActivityResponse>().ReverseMap();
            CreateMap<ActivityTypeOutput, ActivityTypeResponse>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
                .ReverseMap();
        }
    }
}