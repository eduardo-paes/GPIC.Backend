using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.SubArea;
using Application.Ports.SubArea;
using Application.Validation;

namespace Application.UseCases.SubArea
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
            Domain.Entities.SubArea model = await _repository.DeleteAsync(id);

            // Retorna o entidade removido
            return _mapper.Map<DetailedReadSubAreaOutput>(model);
        }
    }
}