using Domain.Contracts.AssistanceType;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases
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
        #endregion

        public async Task<IQueryable<ResumedReadAssistanceTypeOutput>> Execute(int skip, int take)
        {
            var entities = await _repository.GetAll(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadAssistanceTypeOutput>>(entities).AsQueryable();
        }
    }
}