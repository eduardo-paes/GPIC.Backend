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
            var user = _tokenAuthenticationService.GetUserAuthenticatedClaims();

            // Verifica se o usuário logado é um professor ou administrador
            UseCaseException.BusinessRuleViolation(user.Role != ERole.ADMIN && user.Role != ERole.PROFESSOR,
                 "Usuário sem permissão para avaliar projetos.");

            // Obtém todos os projetos que estão na fase de avaliação (Submitted, Evaluation, DocumentAnalysis)
            // e que o usuário logado possa avaliar (somente projetos que o usuário não é o orientador)
            var projects = await _projectRepository.GetProjectsToEvaluateAsync(skip, take, user.Id);

            // Mapeia a lista de projetos para uma lista de projetos resumidos e retorna.
            return _mapper.Map<IList<DetailedReadProjectOutput>>(projects);
        }
    }
}