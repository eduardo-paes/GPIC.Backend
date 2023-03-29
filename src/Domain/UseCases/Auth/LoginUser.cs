using Domain.Contracts.Auth;
using Domain.Interfaces.UseCases.Auth;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.UseCases.Auth
{
    public class LoginUser : ILoginUser
    {
        #region Global Scope
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITokenHandler _tokenHandler;
        public LoginUser(IUserRepository repository, IMapper mapper, ITokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<UserLoginOutput> Execute(UserLoginInput dto)
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

            var model = _mapper.Map<UserLoginOutput>(entity);
            model.Token = _tokenHandler.GenerateToken(model.Id, model.Role);

            return model;
        }
    }
}