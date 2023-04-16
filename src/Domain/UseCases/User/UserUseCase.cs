using AutoMapper;
using Domain.Contracts.User;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases.User
{
    public class UserUseCase
    {
        #region Global Scope
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UserUseCase(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        public async Task<UserReadOutput> GetById(Guid? id)
        {
            var entity = await GetUser(id);
            if (entity == null)
                throw new Exception("Nenhum usuário encontrato para o id informado.");

            return _mapper.Map<UserReadOutput>(entity);
        }

        public async Task<IQueryable<UserReadOutput>> GetActiveUsers(int skip, int take)
        {
            var entities = await _repository.GetActiveUsers(skip, take);
            return _mapper.Map<IEnumerable<UserReadOutput>>(entities).AsQueryable();
        }

        public async Task<IQueryable<UserReadOutput>> GetInactiveUsers(int skip, int take)
        {
            var entities = await _repository.GetInactiveUsers(skip, take);
            return _mapper.Map<IEnumerable<UserReadOutput>>(entities).AsQueryable();
        }

        public async Task<UserReadOutput> Update(Guid? id, UserUpdateInput dto)
        {
            // Recupera usuário que será atualizado
            var user = await GetUser(id);

            // Atualiza atributos permitidos
            user.Name = dto.Name;
            user.CPF = dto.CPF;

            // Salva usuário atualizado no banco
            var entity = await _repository.Update(user);
            return _mapper.Map<UserReadOutput>(entity);
        }

        public async Task<UserReadOutput> Deactivate(Guid id)
        {
            // Encontra usuário e desativa
            var user = await GetUser(id);
            user.DeactivateEntity();

            // Atualiza usuário
            var entity = await _repository.Update(user);
            return _mapper.Map<UserReadOutput>(entity);
        }

        public async Task<UserReadOutput> Activate(Guid id)
        {
            // Encontra usuário e ativa
            var user = await GetUser(id);
            user.ActivateEntity();

            // Atualiza usuário
            var entity = await _repository.Update(user);
            return _mapper.Map<UserReadOutput>(entity);
        }
        #endregion

        #region Private Methods
        private async Task<Entities.User> GetUser(Guid? id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null)
                throw new Exception("Nenhum usuário encontrato para o id informado.");
            return entity;
        }
        #endregion
    }
}