using Domain.Contracts.AssistanceType;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
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
        #endregion

        public async Task<DetailedReadAssistanceTypeOutput> Execute(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Remove a entidade
            var model = await _repository.Delete(id);

            // Retorna o tipo de programa removido
            return _mapper.Map<DetailedReadAssistanceTypeOutput>(model);
        }
    }
}