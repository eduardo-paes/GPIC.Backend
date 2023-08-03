using Adapters.Gateways.Activity;
using AutoMapper;
using Domain.Ports.Activity;
using Domain.UseCases.Ports.Activity;
namespace Adapters.Mappings
{
    public class ActivityMappings : Profile
    {
        public ActivityMappings()
        {
            _ = CreateMap<CreateActivityInput, CreateActivityRequest>().ReverseMap();
            _ = CreateMap<CreateActivityTypeInput, CreateActivityTypeRequest>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
                .ReverseMap();
            _ = CreateMap<UpdateActivityInput, UpdateActivityRequest>().ReverseMap();
            _ = CreateMap<UpdateActivityTypeInput, UpdateActivityTypeRequest>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
                .ForPath(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
                .ReverseMap();
            _ = CreateMap<ActivityOutput, ActivityResponse>().ReverseMap();
            _ = CreateMap<ActivityTypeOutput, ActivityTypeResponse>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities))
                .ReverseMap();
        }
    }
}