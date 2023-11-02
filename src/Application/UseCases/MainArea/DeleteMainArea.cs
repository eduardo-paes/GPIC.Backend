using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.MainArea;
using Application.Ports.MainArea;
using Application.Validation;

namespace Application.UseCases.MainArea
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
        #endregion Global Scope

        public async Task<DetailedMainAreaOutput> ExecuteAsync(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Remove a entidade
            var model = await _repository.DeleteAsync(id);

            // Retorna o edital removido
            return _mapper.Map<DetailedMainAreaOutput>(model);
        }
    }
}