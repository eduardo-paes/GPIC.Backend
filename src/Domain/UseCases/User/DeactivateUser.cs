using AutoMapper;
using Domain.Contracts.User;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;

namespace Domain.UseCases.User
{
    public class DeactivateUser : IDeactivateUser
    {
        #region Global Scope
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public DeactivateUser(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<UserReadOutput> Execute(Guid? id)
        {
            // Verifica se id é nulo
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Encontra usuário pelo Id e o desativa
            var user = await _repository.GetById(id)
                ?? throw new Exception("Nenhum usuário encontrato para o id informado.");
            user.DeactivateEntity();

            // Atualiza usuário
            var entity = await _repository.Update(user);
            return _mapper.Map<UserReadOutput>(entity);
        }
    }
}