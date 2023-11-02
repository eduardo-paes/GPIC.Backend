using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.User;
using Application.Ports.User;
using Application.Validation;

namespace Application.UseCases.User
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

        public async Task<UserReadOutput> ExecuteAsync(Guid? id)
        {
            // Verifica se id é nulo
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Encontra usuário pelo Id e o ativa
            Domain.Entities.User user = await _repository.GetByIdAsync(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.User));
            user.ActivateEntity();

            // Atualiza usuário
            Domain.Entities.User entity = await _repository.UpdateAsync(user);
            return _mapper.Map<UserReadOutput>(entity);
        }
    }
}