using Domain.Contracts.TypeAssistance;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
{
    public class UpdateTypeAssistance : IUpdateTypeAssistance
    {
        #region Global Scope
        private readonly ITypeAssistanceRepository _repository;
        private readonly IMapper _mapper;
        public UpdateTypeAssistance(ITypeAssistanceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadTypeAssistanceOutput> Execute(Guid? id, UpdateTypeAssistanceInput input)
        {
            // Verifica se o id foi informado
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Verifica se nome foi informado
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.Name), nameof(input.Name));

            // Recupera entidade que será atualizada
            var entity = await _repository.GetById(id)
                ?? throw new Exception("Bolsa de Assistência não encontrado.");

            // Verifica se a entidade foi excluída
            if (entity.DeletedAt != null)
                throw new Exception("O Bolsa de Assistência informado já foi excluído.");

            // Verifica se o nome já está sendo usado
            if (!string.Equals(entity.Name, input.Name, StringComparison.OrdinalIgnoreCase)
                && await _repository.GetTypeAssistanceByName(input.Name!) != null)
            {
                throw new Exception("Já existe um Bolsa de Assistência para o nome informado.");
            }

            // Atualiza atributos permitidos
            entity.Name = input.Name;
            entity.Description = input.Description;

            // Salva entidade atualizada no banco
            var model = await _repository.Update(entity);
            return _mapper.Map<DetailedReadTypeAssistanceOutput>(model);
        }
    }
}