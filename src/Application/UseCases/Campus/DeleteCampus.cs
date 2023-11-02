using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Campus;
using Application.Ports.Campus;
using Application.Validation;

namespace Application.UseCases.Campus
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
            var model = await _repository.DeleteAsync(id);

            // Retorna o curso removido
            return _mapper.Map<DetailedReadCampusOutput>(model);
        }
    }
}