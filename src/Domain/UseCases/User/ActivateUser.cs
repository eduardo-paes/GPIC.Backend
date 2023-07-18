using AutoMapper;
using Domain.Contracts.User;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;
using Domain.Validation;

namespace Domain.UseCases
{
    public class ActivateUser : IActivateUser
    {
        #region Global Scope
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public ActivateUser(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<UserReadOutput> Execute(Guid? id)
        {
            // Verifica se id é nulo
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Encontra usuário pelo Id e o ativa
            var user = await _repository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.User));
            user.ActivateEntity();

            // Atualiza usuário
            var entity = await _repository.Update(user);
            return _mapper.Map<UserReadOutput>(entity);
        }
    }
}