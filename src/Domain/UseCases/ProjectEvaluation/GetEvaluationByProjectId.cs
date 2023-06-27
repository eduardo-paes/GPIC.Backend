using AutoMapper;
using Domain.Contracts.ProjectEvaluation;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases.ProjectEvaluation;
using Domain.Validation;

namespace Domain.UseCases.ProjectEvaluation
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
        #endregion

        public async Task<DetailedReadProjectEvaluationOutput> Execute(Guid? projectId)
        {
            // Verifica se Id foi informado.
            UseCaseException.NotInformedParam(projectId is null, nameof(projectId));

            // Obtém a avaliação do projeto pelo Id do projeto.
            var entity = await _repository.GetByProjectId(projectId);

            // Converte e retorna o resultado.
            return _mapper.Map<DetailedReadProjectEvaluationOutput>(entity);
        }
    }
}