using Domain.Contracts.Auth;
using Domain.Contracts.User;
using Domain.Interfaces.UseCases.Auth;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.UseCases;

namespace Domain.UseCases.Auth
{
    public class RegisterUser : IRegisterUser
    {
        #region Global Scope
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public RegisterUser(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<UserReadOutput> Execute(UserRegisterInput dto)
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
            return _mapper.Map<UserReadOutput>(entity);
        }
    }
}