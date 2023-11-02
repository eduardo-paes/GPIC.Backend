using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Area;
using Application.Ports.Area;
using Application.Validation;

namespace Application.UseCases.Area
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

        public async Task<DetailedReadAreaOutput> ExecuteAsync(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id == null, nameof(id));

            // Remove a entidade
            var model = await _repository.DeleteAsync(id);

            // Retorna a área removida
            return _mapper.Map<DetailedReadAreaOutput>(model);
        }
    }
}