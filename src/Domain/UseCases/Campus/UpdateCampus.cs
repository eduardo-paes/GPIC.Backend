using Domain.Contracts.Campus;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases
{
    public class UpdateCampus : IUpdateCampus
    {
        #region Global Scope
        private readonly ICampusRepository _repository;
        private readonly IMapper _mapper;
        public UpdateCampus(ICampusRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadCampusOutput> Execute(Guid? id, UpdateCampusInput input)
        {
            // Verifica se o id foi informado
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Verifica se nome foi informado
            if (string.IsNullOrEmpty(input.Name))
                throw new ArgumentNullException(nameof(input.Name));

            // Recupera entidade que será atualizada
            var entity = await _repository.GetById(id);

            // Verifica se entidade existe
            if (entity == null)
                throw new Exception("Campus não encontrado.");

            // Verifica se a entidade foi excluída
            if (entity.DeletedAt != null)
                throw new Exception("O Campus informado já foi excluído.");

            // Verifica se o nome já está sendo usado
            if (!string.Equals(entity.Name, input.Name, StringComparison.OrdinalIgnoreCase) && await _repository.GetCampusByName(input.Name) != null)
                throw new Exception("Já existe um Campus para o nome informado.");

            // Atualiza atributos permitidos
            entity.Name = input.Name;

            // Salva entidade atualizada no banco
            var model = await _repository.Update(entity);
            return _mapper.Map<DetailedReadCampusOutput>(model);
        }
    }
}