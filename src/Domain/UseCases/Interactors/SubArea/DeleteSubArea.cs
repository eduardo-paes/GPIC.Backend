using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.SubArea;
using Domain.UseCases.Ports.SubArea;
using Domain.Validation;

namespace Domain.UseCases.Interactors.SubArea
{
    public class DeleteSubArea : IDeleteSubArea
    {
        #region Global Scope
        private readonly ISubAreaRepository _repository;
        private readonly IMapper _mapper;
        public DeleteSubArea(ISubAreaRepository subAreaRepository, IMapper mapper)
        {
            _repository = subAreaRepository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadSubAreaOutput> ExecuteAsync(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Remove a entidade
            Entities.SubArea model = await _repository.Delete(id);

            // Retorna o entidade removido
            return _mapper.Map<DetailedReadSubAreaOutput>(model);
        }
    }
}