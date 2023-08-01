using Adapters.Gateways.User;
using AutoMapper;
using Domain.UseCases.Ports.User;

namespace Adapters.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            _ = CreateMap<UserReadOutput, UserReadResponse>().ReverseMap();
            _ = CreateMap<UserUpdateInput, UserUpdateRequest>().ReverseMap();
        }
    }
}