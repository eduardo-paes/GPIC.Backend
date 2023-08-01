using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Campus;
using Domain.UseCases.Ports.Campus;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Campus
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
        #endregion Global Scope

        public async Task<DetailedReadCampusOutput> Execute(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));

            Entities.Campus? entity = await _repository.GetById(id);
            return _mapper.Map<DetailedReadCampusOutput>(entity);
        }
    }
}