using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.UseCases.Interfaces.Project;
using Domain.UseCases.Ports.Project;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Project
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
            Ports.Auth.UserClaimsOutput? userClaims = _tokenAuthenticationService.GetUserAuthenticatedClaims();

            // Se o usuário não estiver autenticado, lança uma exceção.
            UseCaseException.BusinessRuleViolation(userClaims == null || userClaims.Role == null,
                "Usuário não autorizado.");

            // Obtém o tipo de usuário.
            ERole userRole = Enum.Parse<ERole>(userClaims?.Role!);

            // Obtém a lista de projetos de acordo com o tipo de usuário.
            IEnumerable<Entities.Project> projects;

            // Se o usuário for um professor, retorna apenas os seus projetos.
            if (userRole == ERole.PROFESSOR)
            {
                projects = (IEnumerable<Entities.Project>)await _projectRepository.GetProfessorProjectsAsync(skip, take, userClaims?.Id);
            }

            // Se o usuário for um aluno, retorna apenas os seus projetos.
            else if (userRole == ERole.STUDENT)
            {
                projects = (IEnumerable<Entities.Project>)await _projectRepository.GetStudentProjectsAsync(skip, take, userClaims?.Id);
            }

            // Se o usuário for um administrador, permite a busca apenas pelo seu ID.
            else
            {
                projects = userRole == ERole.ADMIN && onlyMyProjects
                    ? (IEnumerable<Entities.Project>)await _projectRepository.GetProfessorProjectsAsync(skip, take, userClaims?.Id)
                    : userRole == ERole.ADMIN && !onlyMyProjects
                                    ? (IEnumerable<Entities.Project>)await _projectRepository.GetProjectsAsync(skip, take)
                                    : throw UseCaseException.BusinessRuleViolation("Usuário não autorizado.");
            }

            // Mapeia a lista de projetos para uma lista de projetos resumidos e retorna.
            return _mapper.Map<IList<ResumedReadProjectOutput>>(projects);
        }
    }
}