using AutoMapper;
using Domain.Contracts.Project;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Interfaces.UseCases.Project;

namespace Domain.UseCases.Project
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
        #endregion

        public async Task<IList<ResumedReadProjectOutput>> Execute(int skip, int take, bool onlyMyProjects = true)
        {
            // Obtém as claims do usuário autenticado.
            var userClaims = _tokenAuthenticationService.GetUserAuthenticatedClaims();

            // Obtém a lista de projetos de acordo com o tipo de usuário.
            IEnumerable<Entities.Project> projects;

            // Se o usuário for um professor, retorna apenas os seus projetos.
            if (userClaims.Role == ERole.PROFESSOR.GetDescription())
                projects = await _projectRepository.GetProfessorProjects(skip, take, userClaims.Id);

            // Se o usuário for um aluno, retorna apenas os seus projetos.
            else if (userClaims.Role == ERole.STUDENT.GetDescription())
                projects = await _projectRepository.GetStudentProjects(skip, take, userClaims.Id);

            // Se o usuário for um administrador, permite a busca apenas pelo seu ID.
            else if (userClaims.Role == ERole.ADMIN.GetDescription() && onlyMyProjects)
                projects = await _projectRepository.GetProfessorProjects(skip, take, userClaims.Id);

            // Se o usuário for um administrador, permite a busca por todos os projetos.
            else if (userClaims.Role == ERole.ADMIN.GetDescription() && !onlyMyProjects)
                projects = await _projectRepository.GetProjects(skip, take);

            // Se o usuário não for nenhum dos tipos acima, lança uma exceção.
            else
                throw new Exception("Usuário não autorizado.");

            // Mapeia a lista de projetos para uma lista de projetos resumidos e retorna.
            return _mapper.Map<IList<ResumedReadProjectOutput>>(projects);
        }
    }
}