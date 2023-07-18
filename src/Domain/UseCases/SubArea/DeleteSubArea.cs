using Domain.Contracts.SubArea;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
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
        #endregion

        public async Task<DetailedReadSubAreaOutput> Execute(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Remove a entidade
            var model = await _repository.Delete(id);

            // Retorna o entidade removido
            return _mapper.Map<DetailedReadSubAreaOutput>(model);
        }
    }
}