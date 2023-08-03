using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Professor;
using Domain.UseCases.Ports.Professor;

namespace Domain.UseCases.Interactors.Professor
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
            IEnumerable<Entities.Professor> entities = (IEnumerable<Entities.Professor>)await _repository.GetAll(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadProfessorOutput>>(entities).AsQueryable();
        }
    }
}