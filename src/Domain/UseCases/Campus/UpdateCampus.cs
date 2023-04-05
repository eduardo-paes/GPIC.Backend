using Domain.Contracts.Campus;
using Domain.Interfaces.UseCases.Campus;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases.Campus
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

        public async Task<DetailedReadCampusOutput> Execute(Guid? id, UpdateCampusInput dto)
        {
            // Verifica se o id foi informado
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Verifica se nome foi informado
            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentNullException(nameof(dto.Name));

            // Recupera entidade que será atualizada
            var entity = await _repository.GetById(id);

            // Verifica se a entidade foi excluída
            if (entity.DeletedAt != null)
                throw new Exception("O Campus informado já foi excluído.");

            // Verifica se o nome já está sendo usado
            if (entity.Name.ToLower() != dto.Name.ToLower()
                && await _repository.GetCampusByName(dto.Name) != null)
                throw new Exception($"Já existe um Campus para o nome informado.");

            // Atualiza atributos permitidos
            entity.Name = dto.Name;

            // Salva entidade atualizada no banco
            var model = await _repository.Update(entity);
            return _mapper.Map<DetailedReadCampusOutput>(model);
        }
    }
}