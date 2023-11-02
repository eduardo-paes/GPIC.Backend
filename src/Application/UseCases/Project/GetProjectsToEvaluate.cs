using Application.Interfaces.UseCases.Project;
using Application.Ports.Project;
using Application.Validation;
using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.UseCases.Project
{
    public class GetProjectsToEvaluate : IGetProjectsToEvaluate
    {
        private readonly ITokenAuthenticationService _tokenAuthenticationService;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public GetProjectsToEvaluate(ITokenAuthenticationService tokenAuthenticationService,
            IProjectRepository projectRepository,
            IMapper mapper)
        {
            _tokenAuthenticationService = tokenAuthenticationService;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<IList<DetailedReadProjectOutput>> ExecuteAsync(int skip, int take)
        {
            // Valida valores de skip e take
            if (skip < 0 || take < 1)
                throw new ArgumentException("Parâmetros inválidos.");

            // Obtém o usuário logado
            var userClaims = _tokenAuthenticationService.GetUserAuthenticatedClaims();

            // Obtém id do usuário e id de acordo com perfil logado
            var userClaim = userClaims!.Values.FirstOrDefault();
            var actorId = userClaims.Keys.FirstOrDefault();

            // Verifica se o usuário logado é um professor ou administrador
            UseCaseException.BusinessRuleViolation(userClaim!.Role != ERole.ADMIN && userClaim.Role != ERole.PROFESSOR,
                 "Usuário sem permissão para avaliar projetos.");

            // Obtém todos os projetos que estão na fase de avaliação (Submitted, Evaluation, DocumentAnalysis)
            // e que o usuário logado possa avaliar (somente projetos que o usuário não é o orientador)
            var projects = await _projectRepository.GetProjectsToEvaluateAsync(skip, take, actorId);

            // Mapeia a lista de projetos para uma lista de projetos resumidos e retorna.
            return _mapper.Map<IList<DetailedReadProjectOutput>>(projects);
        }
    }
}