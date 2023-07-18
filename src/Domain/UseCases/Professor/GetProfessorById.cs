using Domain.Contracts.Professor;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
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
            UseCaseException.NotInformedParam(id is null, nameof(id));
            var entity = await _repository.GetById(id);
            return _mapper.Map<DetailedReadProfessorOutput>(entity);
        }
    }
}