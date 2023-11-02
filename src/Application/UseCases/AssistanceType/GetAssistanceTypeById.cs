using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.AssistanceType;
using Application.Ports.AssistanceType;
using Application.Validation;

namespace Application.UseCases.AssistanceType
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
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<DetailedReadAssistanceTypeOutput>(entity);
        }
    }
}