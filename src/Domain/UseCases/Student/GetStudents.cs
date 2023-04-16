using Domain.Contracts.Student;
using Domain.Interfaces.UseCases.Student;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases.Student
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
        #endregion

        public async Task<IQueryable<ResumedReadStudentOutput>> Execute(int skip, int take)
        {
            var entities = await _repository.GetAll(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadStudentOutput>>(entities).AsQueryable();
        }
    }
}