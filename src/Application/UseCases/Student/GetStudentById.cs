using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Student;
using Application.Ports.Student;
using Application.Validation;

namespace Application.UseCases.Student
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

        public async Task<DetailedReadStudentOutput> ExecuteAsync(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));
            Domain.Entities.Student? entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<DetailedReadStudentOutput>(entity);
        }
    }
}