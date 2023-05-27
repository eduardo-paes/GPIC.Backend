using Domain.Contracts.StudentAssistanceScholarship;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases
{
    public class GetStudentAssistanceScholarshipById : IGetStudentAssistanceScholarshipById
    {
        #region Global Scope
        private readonly IStudentAssistanceScholarshipRepository _repository;
        private readonly IMapper _mapper;
        public GetStudentAssistanceScholarshipById(IStudentAssistanceScholarshipRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadStudentAssistanceScholarshipOutput> Execute(Guid? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var entity = await _repository.GetById(id);
            return _mapper.Map<DetailedReadStudentAssistanceScholarshipOutput>(entity);
        }
    }
}