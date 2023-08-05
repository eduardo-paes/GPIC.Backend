using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Campus;
using Application.Ports.Campus;
using Application.Validation;

namespace Application.UseCases.Campus
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

        public async Task<DetailedReadCampusOutput> ExecuteAsync(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<DetailedReadCampusOutput>(entity);
        }
    }
}