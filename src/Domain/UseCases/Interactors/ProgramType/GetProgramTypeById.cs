using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.ProgramType;
using Domain.UseCases.Ports.ProgramType;
using Domain.Validation;

namespace Domain.UseCases.Interactors.ProgramType
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
        #endregion Global Scope

        public async Task<DetailedReadProgramTypeOutput> Execute(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));
            Entities.ProgramType? entity = await _repository.GetById(id);
            return _mapper.Map<DetailedReadProgramTypeOutput>(entity);
        }
    }
}