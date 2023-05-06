using AutoMapper;
using Adapters.DTOs.User;
using Domain.Contracts.User;

namespace Adapters.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<UserReadOutput, UserReadDTO>().ReverseMap();
            CreateMap<UserUpdateInput, UserUpdateDTO>().ReverseMap();
        }
    }
}