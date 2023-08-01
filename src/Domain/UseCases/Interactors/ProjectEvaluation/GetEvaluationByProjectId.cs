using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.ProjectEvaluation;
using Domain.UseCases.Ports.ProjectEvaluation;
using Domain.Validation;

namespace Domain.UseCases.Interactors.ProjectEvaluation
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

        public async Task<DetailedReadProjectEvaluationOutput> Execute(Guid? projectId)
        {
            // Verifica se Id foi informado.
            UseCaseException.NotInformedParam(projectId is null, nameof(projectId));

            // Obtém a avaliação do projeto pelo Id do projeto.
            Entities.ProjectEvaluation? entity = await _repository.GetByProjectId(projectId);

            // Converte e retorna o resultado.
            return _mapper.Map<DetailedReadProjectEvaluationOutput>(entity);
        }
    }
}