using AutoMapper;
using Domain.Contracts.User;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;
using Domain.Validation;

namespace Domain.UseCases
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
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Encontra usuário pelo Id e o desativa
            var user = await _repository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.User));
            user.DeactivateEntity();

            // Atualiza usuário
            var entity = await _repository.Update(user);
            return _mapper.Map<UserReadOutput>(entity);
        }
    }
}