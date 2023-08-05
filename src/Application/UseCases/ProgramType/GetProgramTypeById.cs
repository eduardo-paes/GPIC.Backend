using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.ProgramType;
using Application.Ports.ProgramType;
using Application.Validation;

namespace Application.UseCases.ProgramType
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

        public async Task<DetailedReadProgramTypeOutput> ExecuteAsync(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<DetailedReadProgramTypeOutput>(entity);
        }
    }
}