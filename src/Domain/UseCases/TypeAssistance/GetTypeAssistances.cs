using Domain.Contracts.TypeAssistance;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases
{
    public class GetTypeAssistances : IGetTypeAssistances
    {
        #region Global Scope
        private readonly ITypeAssistanceRepository _repository;
        private readonly IMapper _mapper;
        public GetTypeAssistances(ITypeAssistanceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<IQueryable<ResumedReadTypeAssistanceOutput>> Execute(int skip, int take)
        {
            var entities = await _repository.GetAll(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadTypeAssistanceOutput>>(entities).AsQueryable();
        }
    }
}