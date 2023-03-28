using Domain.Contracts.MainArea;
using Domain.Interfaces.UseCases.MainArea;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases.MainArea
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
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var entity = await _repository.GetById(id);
            return _mapper.Map<DetailedMainAreaOutput>(entity);
        }
    }
}