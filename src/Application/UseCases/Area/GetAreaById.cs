using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Area;
using Application.Ports.Area;
using Application.Validation;

namespace Application.UseCases.Area
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

        public async Task<DetailedReadAreaOutput> ExecuteAsync(Guid? id)
        {
            // Verifica se Id foi informado.
            UseCaseException.NotInformedParam(id is null, nameof(id));

            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<DetailedReadAreaOutput>(entity);
        }
    }
}