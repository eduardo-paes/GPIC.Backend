using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.AssistanceType;
using Application.Ports.AssistanceType;
using Application.Validation;

namespace Application.UseCases.AssistanceType
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
            var model = await _repository.DeleteAsync(id);

            // Retorna o tipo de programa removido
            return _mapper.Map<DetailedReadAssistanceTypeOutput>(model);
        }
    }
}