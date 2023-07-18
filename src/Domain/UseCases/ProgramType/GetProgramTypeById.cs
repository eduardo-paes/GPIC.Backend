using Domain.Contracts.ProgramType;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
{
    public class GetProgramTypeById : IGetProgramTypeById
    {
        #region Global Scope
        private readonly IProgramTypeRepository _repository;
        private readonly IMapper _mapper;
        public GetProgramTypeById(IProgramTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadProgramTypeOutput> Execute(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));
            var entity = await _repository.GetById(id);
            return _mapper.Map<DetailedReadProgramTypeOutput>(entity);
        }
    }
}