using AutoMapper;
using Adapters.Gateways.User;
using Domain.Contracts.User;

namespace Adapters.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<UserReadOutput, UserReadResponse>().ReverseMap();
            CreateMap<UserUpdateInput, UserUpdateRequest>().ReverseMap();
        }
    }
}