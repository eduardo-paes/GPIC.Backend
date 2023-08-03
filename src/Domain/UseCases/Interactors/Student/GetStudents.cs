using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Student;
using Domain.UseCases.Ports.Student;

namespace Domain.UseCases.Interactors.Student
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
            IEnumerable<Entities.Student> entities = (IEnumerable<Entities.Student>)await _repository.GetAllAsync(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadStudentOutput>>(entities).AsQueryable();
        }
    }
}