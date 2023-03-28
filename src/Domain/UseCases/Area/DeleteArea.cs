using Domain.Contracts.Area;
using Domain.Interfaces.UseCases.Area;
using AutoMapper;
using Domain.Interfaces.Repositories;

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

        public async Task<DetailedReadAreaOutput> Execute(Guid? id)
        {
            if (id == null)
                throw new Exception("O Id da Área não pode ser vazio.");

            var model = await _repository.Delete(id);
            return _mapper.Map<DetailedReadAreaOutput>(model);
        }
    }
}