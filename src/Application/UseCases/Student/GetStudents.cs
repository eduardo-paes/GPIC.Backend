using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Student;
using Application.Ports.Student;

namespace Application.UseCases.Student
{
    public class GetStudents : IGetStudents
    {
        #region Global Scope
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;
        public GetStudents(IStudentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IQueryable<ResumedReadStudentOutput>> ExecuteAsync(int skip, int take)
        {
            IEnumerable<Domain.Entities.Student> entities = await _repository.GetAllAsync(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadStudentOutput>>(entities).AsQueryable();
        }
    }
}