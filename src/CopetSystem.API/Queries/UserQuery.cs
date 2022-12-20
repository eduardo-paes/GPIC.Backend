using System;
using CopetSystem.Application.DTOs;
using CopetSystem.Application.Interfaces;

namespace CopetSystem.API.Queries
{
    public class UserQuery
    {
        private readonly IUserService _service;
        public UserQuery(IUserService service)
        {
            _service = service;
        }

        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Gets the queryable users.")]
        public async Task<IQueryable<UserReadDTO>> GetUsers([Service] IUserService service)
        {
            return await service.GetActiveUsers();
        }
    }
}