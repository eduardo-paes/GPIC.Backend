using Adapters.Gateways.Activity;
using AutoMapper;
using Domain.Contracts.Activity;

namespace Adapters.Mappings
{
    public class ActivityMappings : Profile
    {
        public ActivityMappings()
        {
            CreateMap<CreateActivityInput, CreateActivityRequest>();
            CreateMap<CreateActivityTypeInput, CreateActivityTypeRequest>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities));

            CreateMap<UpdateActivityInput, UpdateActivityRequest>();
            CreateMap<UpdateActivityTypeInput, UpdateActivityTypeRequest>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities));

            CreateMap<ActivityOutput, ActivityResponse>();
            CreateMap<ActivityTypeOutput, ActivityTypeResponse>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities));
        }
    }
}