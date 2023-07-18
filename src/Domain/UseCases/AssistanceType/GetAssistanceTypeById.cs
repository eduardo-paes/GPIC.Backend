using Domain.Contracts.AssistanceType;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
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
        #endregion

        public async Task<DetailedReadAssistanceTypeOutput> Execute(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));
            var entity = await _repository.GetById(id);
            return _mapper.Map<DetailedReadAssistanceTypeOutput>(entity);
        }
    }
}