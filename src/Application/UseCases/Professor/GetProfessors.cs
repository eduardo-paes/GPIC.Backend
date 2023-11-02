using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Professor;
using Application.Ports.Professor;

namespace Application.UseCases.Professor
{
    public class GetProfessors : IGetProfessors
    {
        #region Global Scope
        private readonly IProfessorRepository _repository;
        private readonly IMapper _mapper;
        public GetProfessors(IProfessorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IQueryable<ResumedReadProfessorOutput>> ExecuteAsync(int skip, int take)
        {
            // Valida valores de skip e take
            if (skip < 0 || take < 1)
                throw new ArgumentException("Parâmetros inválidos.");

            var entities = await _repository.GetAllAsync(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadProfessorOutput>>(entities).AsQueryable();
        }
    }
}