using Domain.Contracts.Area;
using Domain.Interfaces.UseCases.Area;
using AutoMapper;
using Domain.Interfaces.UseCases;

namespace Domain.UseCases.Area
{
    public class DeleteArea : IDeleteArea
    {
        #region Global Scope
        private readonly IAreaRepository _repository;
        private readonly IMapper _mapper;
        public DeleteArea(IAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadAreaOutput> Execute(Guid id)
        {
            var model = await _repository.Delete(id);
            return _mapper.Map<DetailedReadAreaOutput>(model);
        }
    }
}