using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Application.Interfaces.UseCases.Project;
using Application.Ports.Project;
using Application.Validation;

namespace Application.UseCases.Project
{
    public class GetOpenProjects : IGetOpenProjects
    {
        #region Global Scope
        private readonly IProjectRepository _projectRepository;
        private readonly ITokenAuthenticationService _tokenAuthenticationService;
        private readonly IMapper _mapper;
        public GetOpenProjects(IProjectRepository projectRepository,
            ITokenAuthenticationService tokenAuthenticationService,
            IMapper mapper)
        {
            _projectRepository = projectRepository;
            _tokenAuthenticationService = tokenAuthenticationService;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IList<ResumedReadProjectOutput>> ExecuteAsync(int skip, int take, bool onlyMyProjects = true)
        {
            // Obtém as claims do usuário autenticado.
            var user = _tokenAuthenticationService.GetUserAuthenticatedClaims();

            // Se o usuário não estiver autenticado, lança uma exceção.
            UseCaseException.BusinessRuleViolation(user == null || user.Role == null,
                "Usuário não autorizado.");

            // Obtém a lista de projetos de acordo com o tipo de usuário.
            IEnumerable<Domain.Entities.Project> projects;

            // Se o usuário for um professor, retorna apenas os seus projetos.
            if (user?.Role == ERole.PROFESSOR)
            {
                projects = await _projectRepository.GetProfessorProjectsAsync(skip, take, user?.Id);
            }

            // Se o usuário for um aluno, retorna apenas os seus projetos.
            else if (user?.Role == ERole.STUDENT)
            {
                projects = await _projectRepository.GetStudentProjectsAsync(skip, take, user?.Id);
            }

            // Se o usuário for um administrador, permite a busca apenas pelo seu ID.
            else
            {
                projects = user?.Role == ERole.ADMIN && onlyMyProjects
                    ? await _projectRepository.GetProfessorProjectsAsync(skip, take, user?.Id)
                    : user?.Role == ERole.ADMIN && !onlyMyProjects
                                    ? await _projectRepository.GetProjectsAsync(skip, take)
                                    : throw UseCaseException.BusinessRuleViolation("Usuário não autorizado.");
            }

            // Mapeia a lista de projetos para uma lista de projetos resumidos e retorna.
            return _mapper.Map<IList<ResumedReadProjectOutput>>(projects);
        }
    }
}