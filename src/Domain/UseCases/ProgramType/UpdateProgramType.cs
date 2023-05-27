using Domain.Contracts.ProgramType;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases
{
    public class UpdateProgramType : IUpdateProgramType
    {
        #region Global Scope
        private readonly IProgramTypeRepository _repository;
        private readonly IMapper _mapper;
        public UpdateProgramType(IProgramTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadProgramTypeOutput> Execute(Guid? id, UpdateProgramTypeInput input)
        {
            // Verifica se o id foi informado
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Verifica se nome foi informado
            if (string.IsNullOrEmpty(input.Name))
                throw new ArgumentNullException(nameof(input.Name));

            // Recupera entidade que será atualizada
            var entity = await _repository.GetById(id);

            // Verifica se a entidade foi encontrada
            if (entity == null)
                throw new Exception("Tipo de Programa não encontrado.");

            // Verifica se a entidade foi excluída
            if (entity.DeletedAt != null)
                throw new Exception("O Tipo de Programa informado já foi excluído.");

            // Verifica se o nome já está sendo usado
            if (!string.Equals(entity.Name, input.Name, StringComparison.OrdinalIgnoreCase)
                && await _repository.GetProgramTypeByName(input.Name) != null)
                throw new Exception($"Já existe um Tipo de Programa para o nome informado.");

            // Atualiza atributos permitidos
            entity.Name = input.Name;
            entity.Description = input.Description;

            // Salva entidade atualizada no banco
            var model = await _repository.Update(entity);
            return _mapper.Map<DetailedReadProgramTypeOutput>(model);
        }
    }
}