using AutoMapper;
using Domain.Entities;
using Domain.UseCases.Ports.User;

namespace Domain.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            _ = CreateMap<User, UserReadOutput>().ReverseMap();
            _ = CreateMap<User, UserUpdateInput>().ReverseMap();
        }
    }
}