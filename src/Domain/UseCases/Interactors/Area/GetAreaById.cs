using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Area;
using Domain.UseCases.Ports.Area;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Area
{
    public class GetAreaById : IGetAreaById
    {
        #region Global Scope
        private readonly IAreaRepository _repository;
        private readonly IMapper _mapper;
        public GetAreaById(IAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadAreaOutput> Execute(Guid? id)
        {
            // Verifica se Id foi informado.
            UseCaseException.NotInformedParam(id is null, nameof(id));

            Entities.Area? entity = await _repository.GetById(id);
            return _mapper.Map<DetailedReadAreaOutput>(entity);
        }
    }
}