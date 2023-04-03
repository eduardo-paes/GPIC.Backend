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
            // Verifica se o id foi informado
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Remove a entidade
            var model = await _repository.Delete(id);

            // Retorna a área removida
            return _mapper.Map<DetailedReadAreaOutput>(model);
        }
    }
}