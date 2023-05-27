using Domain.Contracts.StudentAssistanceScholarship;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases
{
    public class GetStudentAssistanceScholarships : IGetStudentAssistanceScholarships
    {
        #region Global Scope
        private readonly IStudentAssistanceScholarshipRepository _repository;
        private readonly IMapper _mapper;
        public GetStudentAssistanceScholarships(IStudentAssistanceScholarshipRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<IQueryable<ResumedReadStudentAssistanceScholarshipOutput>> Execute(int skip, int take)
        {
            var entities = await _repository.GetAll(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadStudentAssistanceScholarshipOutput>>(entities).AsQueryable();
        }
    }
}