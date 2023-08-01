using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.User;
using Domain.UseCases.Ports.User;
using Domain.Validation;

namespace Domain.UseCases.Interactors.User
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
        #endregion Global Scope

        public async Task<UserReadOutput> Execute(Guid? id)
        {
            // Verifica se id é nulo
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Encontra usuário pelo Id e o ativa
            Entities.User user = await _repository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.User));
            user.ActivateEntity();

            // Atualiza usuário
            Entities.User entity = await _repository.Update(user);
            return _mapper.Map<UserReadOutput>(entity);
        }
    }
}