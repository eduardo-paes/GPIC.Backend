using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.MainArea;
using Domain.UseCases.Ports.MainArea;
using Domain.Validation;

namespace Domain.UseCases.Interactors.MainArea
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
        #endregion Global Scope

        public async Task<DetailedMainAreaOutput> ExecuteAsync(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));

            Entities.MainArea? entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<DetailedMainAreaOutput>(entity);
        }
    }
}