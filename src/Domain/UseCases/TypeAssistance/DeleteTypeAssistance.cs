using Domain.Contracts.TypeAssistance;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases
{
    public class DeleteTypeAssistance : IDeleteTypeAssistance
    {
        #region Global Scope
        private readonly ITypeAssistanceRepository _repository;
        private readonly IMapper _mapper;
        public DeleteTypeAssistance(ITypeAssistanceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadTypeAssistanceOutput> Execute(Guid? id)
        {
            // Verifica se o id foi informado
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Remove a entidade
            var model = await _repository.Delete(id);

            // Retorna o tipo de programa removido
            return _mapper.Map<DetailedReadTypeAssistanceOutput>(model);
        }
    }
}