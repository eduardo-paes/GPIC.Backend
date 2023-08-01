using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.AssistanceType;
using Domain.UseCases.Ports.AssistanceType;

namespace Domain.UseCases.Interactors.AssistanceType
{
    public class GetAssistanceTypes : IGetAssistanceTypes
    {
        #region Global Scope
        private readonly IAssistanceTypeRepository _repository;
        private readonly IMapper _mapper;
        public GetAssistanceTypes(IAssistanceTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IQueryable<ResumedReadAssistanceTypeOutput>> Execute(int skip, int take)
        {
            IEnumerable<Entities.AssistanceType> entities = (IEnumerable<Entities.AssistanceType>)await _repository.GetAll(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadAssistanceTypeOutput>>(entities).AsQueryable();
        }
    }
}