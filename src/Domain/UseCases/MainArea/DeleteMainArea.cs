using Domain.Contracts.MainArea;
using Domain.Interfaces.UseCases.MainArea;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases.MainArea
{
    public class DeleteMainArea : IDeleteMainArea
    {
        #region Global Scope
        private readonly IMainAreaRepository _repository;
        private readonly IMapper _mapper;
        public DeleteMainArea(IMainAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedMainAreaOutput> Execute(Guid? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var model = await _repository.Delete(id);
            return _mapper.Map<DetailedMainAreaOutput>(model);
        }
    }
}