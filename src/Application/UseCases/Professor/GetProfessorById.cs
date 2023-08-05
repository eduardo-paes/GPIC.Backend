using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Professor;
using Application.Ports.Professor;
using Application.Validation;

namespace Application.UseCases.Professor
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
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<DetailedReadProfessorOutput>(entity);
        }
    }
}