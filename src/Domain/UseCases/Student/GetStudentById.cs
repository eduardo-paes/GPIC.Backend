using Domain.Contracts.Student;
using Domain.Interfaces.UseCases.Student;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases.Student
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
        #endregion

        public async Task<DetailedReadStudentOutput> Execute(Guid? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var entity = await _repository.GetById(id);
            return _mapper.Map<DetailedReadStudentOutput>(entity);
        }
    }
}