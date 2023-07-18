using Domain.Contracts.MainArea;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
{
    public class GetMainAreaById : IGetMainAreaById
    {
        #region Global Scope
        private readonly IMainAreaRepository _repository;
        private readonly IMapper _mapper;
        public GetMainAreaById(IMainAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedMainAreaOutput> Execute(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));

            var entity = await _repository.GetById(id);
            return _mapper.Map<DetailedMainAreaOutput>(entity);
        }
    }
}