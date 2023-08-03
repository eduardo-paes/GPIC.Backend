using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.AssistanceType;
using Domain.UseCases.Ports.AssistanceType;
using Domain.Validation;

namespace Domain.UseCases.Interactors.AssistanceType
{
    public class DeleteAssistanceType : IDeleteAssistanceType
    {
        #region Global Scope
        private readonly IAssistanceTypeRepository _repository;
        private readonly IMapper _mapper;
        public DeleteAssistanceType(IAssistanceTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadAssistanceTypeOutput> ExecuteAsync(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Remove a entidade
            Entities.AssistanceType model = await _repository.Delete(id);

            // Retorna o tipo de programa removido
            return _mapper.Map<DetailedReadAssistanceTypeOutput>(model);
        }
    }
}