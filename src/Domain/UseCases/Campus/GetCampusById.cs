using Domain.Contracts.Campus;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
{
    public class GetCampusById : IGetCampusById
    {
        #region Global Scope
        private readonly ICampusRepository _repository;
        private readonly IMapper _mapper;
        public GetCampusById(ICampusRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadCampusOutput> Execute(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));

            var entity = await _repository.GetById(id);
            return _mapper.Map<DetailedReadCampusOutput>(entity);
        }
    }
}