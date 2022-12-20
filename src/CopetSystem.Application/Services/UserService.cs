using System;
using AutoMapper;
using CopetSystem.Application.DTOs;
using CopetSystem.Application.Interfaces;
using CopetSystem.Domain.Interfaces;

namespace CopetSystem.Application.Services
{
	public class UserService : IUserService
	{
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IQueryable<UserReadDTO>> GetActiveUsers()
        {
            var entities = await _repository.GetActiveUsers();
            return _mapper.Map<IEnumerable<UserReadDTO>>(entities).AsQueryable();
        }

        public async Task<UserReadDTO> GetByEmail(string? email)
        {
            var entity = await _repository.GetByEmail(email);
            return _mapper.Map<UserReadDTO>(entity);
        }

        public async Task<UserReadDTO> GetById(int? id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<UserReadDTO>(entity);
        }

        public async Task<IQueryable<UserReadDTO>> GetInactiveUsers()
        {
            var entities = await _repository.GetInactiveUsers();
            return _mapper.Map<IEnumerable<UserReadDTO>>(entities).AsQueryable();
        }
    }
}

