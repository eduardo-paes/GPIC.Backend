using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Student;
using Domain.UseCases.Ports.Student;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Student
{
    public class GetStudentById : IGetStudentById
    {
        #region Global Scope
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;
        public GetStudentById(IStudentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadStudentOutput> Execute(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));
            Entities.Student? entity = await _repository.GetById(id);
            return _mapper.Map<DetailedReadStudentOutput>(entity);
        }
    }
}