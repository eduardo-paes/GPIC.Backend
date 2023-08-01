using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Area;
using Domain.UseCases.Ports.Area;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Area
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
        #endregion Global Scope

        public async Task<DetailedReadAreaOutput> Execute(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id == null, nameof(id));

            // Remove a entidade
            Entities.Area model = await _repository.Delete(id);

            // Retorna a área removida
            return _mapper.Map<DetailedReadAreaOutput>(model);
        }
    }
}