using AutoMapper;
using Domain.Contracts.User;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;

namespace Domain.UseCases.User
{
    public class UpdateUser : IUpdateUser
    {
        #region Global Scope
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UpdateUser(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<UserReadOutput> Execute(Guid? id, UserUpdateInput dto)
        {
            // Verifica se o id informado é nulo
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Busca usuário pelo id informado
            var user = await _repository.GetById(id)
                ?? throw new Exception("Nenhum usuário encontrato para o id informado.");

            // Atualiza atributos permitidos
            user.Name = dto.Name;
            user.CPF = dto.CPF;

            // Salva usuário atualizado no banco
            var entity = await _repository.Update(user);
            return _mapper.Map<UserReadOutput>(entity);
        }
    }
}