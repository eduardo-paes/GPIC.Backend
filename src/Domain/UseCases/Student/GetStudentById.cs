using Domain.Contracts.Student;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
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
            UseCaseException.NotInformedParam(id is null, nameof(id));
            var entity = await _repository.GetById(id);
            return _mapper.Map<DetailedReadStudentOutput>(entity);
        }
    }
}