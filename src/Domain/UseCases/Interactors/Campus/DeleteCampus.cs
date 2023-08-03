using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Campus;
using Domain.UseCases.Ports.Campus;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Campus
{
    public class DeleteCampus : IDeleteCampus
    {
        #region Global Scope
        private readonly ICampusRepository _repository;
        private readonly IMapper _mapper;
        public DeleteCampus(ICampusRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadCampusOutput> ExecuteAsync(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Remove a entidade
            Entities.Campus model = await _repository.Delete(id);

            // Retorna o curso removido
            return _mapper.Map<DetailedReadCampusOutput>(model);
        }
    }
}