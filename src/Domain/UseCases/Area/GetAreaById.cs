using Domain.Contracts.Area;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
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
        #endregion

        public async Task<DetailedReadAreaOutput> Execute(Guid? id)
        {
            // Verifica se Id foi informado.
            if (id is null)
                throw UseCaseException.NotInformedParam(nameof(id));

            var entity = await _repository.GetById(id);
            return _mapper.Map<DetailedReadAreaOutput>(entity);
        }
    }
}