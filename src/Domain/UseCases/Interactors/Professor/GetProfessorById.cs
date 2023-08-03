using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Professor;
using Domain.UseCases.Ports.Professor;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Professor
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
        #endregion Global Scope

        public async Task<DetailedReadProfessorOutput> ExecuteAsync(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));
            Entities.Professor? entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<DetailedReadProfessorOutput>(entity);
        }
    }
}