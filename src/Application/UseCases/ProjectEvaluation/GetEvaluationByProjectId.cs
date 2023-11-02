using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.ProjectEvaluation;
using Application.Ports.ProjectEvaluation;
using Application.Validation;

namespace Application.UseCases.ProjectEvaluation
{
    public class GetEvaluationByProjectId : IGetEvaluationByProjectId
    {
        #region Global Scope
        private readonly IMapper _mapper;
        private readonly IProjectEvaluationRepository _repository;
        public GetEvaluationByProjectId(IMapper mapper,
            IProjectEvaluationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        #endregion Global Scope

        public async Task<DetailedReadProjectEvaluationOutput> ExecuteAsync(Guid? projectId)
        {
            UseCaseException.NotInformedParam(projectId is null, nameof(projectId));
            var entity = await _repository.GetByProjectIdAsync(projectId);
            return _mapper.Map<DetailedReadProjectEvaluationOutput>(entity);
        }
    }
}