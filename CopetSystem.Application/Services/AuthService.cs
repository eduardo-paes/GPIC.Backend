using System;
using AutoMapper;
using CopetSystem.Application.DTOs.Auth;
using CopetSystem.Application.DTOs.User;
using CopetSystem.Application.Interfaces;
using CopetSystem.Domain.Entities;
using CopetSystem.Domain.Interfaces;

namespace CopetSystem.Application.Services
{
	public class AuthService : IAuthService
    {
        #region Global Scope
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public AuthService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        public async Task<UserReadDTO> Register(UserRegisterDTO dto)
        {
            // Valida se já existe usuário para o CPF informado
            var entity = await _repository.GetUserByCPF(dto.CPF);
            if (entity != null)
                throw new Exception("Já existe um usuário com o CPF informado informado.");

            // Valida se já existe usuário para o e-mail informado
            entity = await _repository.GetUserByEmail(dto.Email);
            if (entity != null)
                throw new Exception("Já existe um usuário com o Email informado informado.");

            // Realiza criação do usuário
            entity = await _repository.Register(_mapper.Map<User>(dto));
            return _mapper.Map<UserReadDTO>(entity);
        }

        public async Task<UserLoginResponseDTO> Login(UserLoginRequestDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Email))
                throw new Exception("Email não informado.");

            if (string.IsNullOrEmpty(dto.Password))
                throw new Exception("Senha não informada.");

            var entity = await _repository.GetUserByEmail(dto.Email);
            if (entity == null)
                throw new Exception("Nenhum usuário encontrado.");

            if (!entity.Password.Equals(dto.Password))
                throw new Exception("Credenciais inválidas.");

            var model = _mapper.Map<UserLoginResponseDTO>(entity);
            model.Token = GenerateToken(model.Id, model.Role);

            return model;
        }

        public async Task<bool> ResetPassword(UserResetPasswordDTO dto)
        {
            try
            {
                var entity = await GetUser(dto.Id);
                entity.UpdatePassword(dto.Password);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }
        #endregion

        #region Private Methods
        private async Task<User> GetUser(Guid? id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null)
                throw new Exception("Nenhum usuário encontrato para o id informado.");
            return entity;
        }

        private string GenerateToken(Guid? id, string? role)
        {
            return "Token";
        }
        #endregion
    }
}

