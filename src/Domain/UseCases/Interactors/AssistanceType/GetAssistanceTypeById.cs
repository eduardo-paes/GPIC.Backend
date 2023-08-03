using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.AssistanceType;
using Domain.UseCases.Ports.AssistanceType;
using Domain.Validation;

namespace Domain.UseCases.Interactors.AssistanceType
{
    public class GetAssistanceTypeById : IGetAssistanceTypeById
    {
        #region Global Scope
        private readonly IAssistanceTypeRepository _repository;
        private readonly IMapper _mapper;
        public GetAssistanceTypeById(IAssistanceTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadAssistanceTypeOutput> ExecuteAsync(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));
            Entities.AssistanceType? entity = await _repository.GetById(id);
            return _mapper.Map<DetailedReadAssistanceTypeOutput>(entity);
        }
    }
}