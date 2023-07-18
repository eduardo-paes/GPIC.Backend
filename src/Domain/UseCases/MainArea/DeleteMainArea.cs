using Domain.Contracts.MainArea;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
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
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Remove a entidade
            var model = await _repository.Delete(id);

            // Retorna o edital removido
            return _mapper.Map<DetailedMainAreaOutput>(model);
        }
    }
}