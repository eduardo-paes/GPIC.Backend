using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Campus;
using Application.Ports.Campus;

namespace Application.UseCases.Campus
{
    public class GetCampuses : IGetCampuses
    {
        #region Global Scope
        private readonly ICampusRepository _repository;
        private readonly IMapper _mapper;
        public GetCampuses(ICampusRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IQueryable<ResumedReadCampusOutput>> ExecuteAsync(int skip, int take)
        {
            // Valida valores de skip e take
            if (skip < 0 || take < 1)
                throw new ArgumentException("Parâmetros inválidos.");

            var entities = await _repository.GetAllAsync(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadCampusOutput>>(entities).AsQueryable();
        }
    }
}