using Domain.Contracts.Professor;
using Domain.Interfaces.UseCases.Professor;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases.Professor
{
    public class GetProfessorById : IGetProfessorById
    {
        #region Global Scope
        private readonly IProfessorRepository _repository;
        private readonly IMapper _mapper;
        public GetProfessorById(IProfessorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadProfessorOutput> Execute(Guid? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var entity = await _repository.GetById(id);
            return _mapper.Map<DetailedReadProfessorOutput>(entity);
        }
    }
}