using AutoMapper;
using Application.DTOs.User;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService
    {
        #region Global Scope
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        public async Task<UserReadDTO> GetById(Guid? id)
        {
            var entity = await GetUser(id);
            if (entity == null)
                throw new Exception("Nenhum usuário encontrato para o id informado.");

            return _mapper.Map<UserReadDTO>(entity);
        }

        public async Task<IQueryable<UserReadDTO>> GetActiveUsers(int skip, int take)
        {
            var entities = await _repository.GetActiveUsers(skip, take);
            return _mapper.Map<IEnumerable<UserReadDTO>>(entities).AsQueryable();
        }

        public async Task<IQueryable<UserReadDTO>> GetInactiveUsers(int skip, int take)
        {
            var entities = await _repository.GetInactiveUsers(skip, take);
            return _mapper.Map<IEnumerable<UserReadDTO>>(entities).AsQueryable();
        }

        public async Task<UserReadDTO> Update(Guid? id, UserUpdateDTO dto)
        {
            // Recupera usuário que será atualizado
            var user = await GetUser(id);

            // Atualiza atributos permitidos
            user.UpdateName(dto.Name);
            user.UpdateRole(dto.Role);
            user.UpdateCPF(dto.CPF);

            // Salva usuário atualizado no banco
            var entity = await _repository.Update(user);
            return _mapper.Map<UserReadDTO>(entity);
        }

        public async Task<UserReadDTO> Deactivate(Guid id)
        {
            // Encontra usuário e desativa
            var user = await GetUser(id);
            user.DeactivateEntity();

            // Atualiza usuário
            var entity = await _repository.Update(user);
            return _mapper.Map<UserReadDTO>(entity);
        }

        public async Task<UserReadDTO> Activate(Guid id)
        {
            // Encontra usuário e ativa
            var user = await GetUser(id);
            user.ActivateEntity();

            // Atualiza usuário
            var entity = await _repository.Update(user);
            return _mapper.Map<UserReadDTO>(entity);
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
        #endregion
    }
}

