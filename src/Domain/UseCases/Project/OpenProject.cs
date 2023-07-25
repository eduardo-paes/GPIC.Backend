using AutoMapper;
using Domain.Contracts.Project;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases.Project;

namespace Domain.UseCases.Project;
public class OpenProject : IOpenProject
{
    #region Global Scope
    private readonly IProjectRepository _projectRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IProfessorRepository _professorRepository;
    private readonly INoticeRepository _noticeRepository;
    private readonly ISubAreaRepository _subAreaRepository;
    private readonly IProgramTypeRepository _programTypeRepository;
    private readonly IMapper _mapper;
    public OpenProject(IProjectRepository projectRepository,
        IStudentRepository studentRepository,
        IProfessorRepository professorRepository,
        INoticeRepository noticeRepository,
        ISubAreaRepository subAreaRepository,
        IProgramTypeRepository programTypeRepository,
        IMapper mapper)
    {
        _projectRepository = projectRepository;
        _studentRepository = studentRepository;
        _professorRepository = professorRepository;
        _noticeRepository = noticeRepository;
        _subAreaRepository = subAreaRepository;
        _programTypeRepository = programTypeRepository;
        _mapper = mapper;
    }
    #endregion

    public async Task<ResumedReadProjectOutput> Execute(OpenProjectInput input)
    {
        // Mapeia input para entidade e realiza validação dos campos informados
        var project = new Entities.Project(
            input.Title,
            input.KeyWord1,
            input.KeyWord2,
            input.KeyWord3,
            input.IsScholarshipCandidate,
            input.Objective,
            input.Methodology,
            input.ExpectedResults,
            input.ActivitiesExecutionSchedule,
            input.StudentId,
            input.ProgramTypeId,
            input.ProfessorId,
            input.SubAreaId,
            input.NoticeId,
            EProjectStatus.Opened,
            EProjectStatus.Opened.GetDescription(),
            null,
            DateTime.UtcNow,
            null,
            null,
            null);

        // Verifica se Edital existe
        var notice = await _noticeRepository.GetById(project.NoticeId)
            ?? throw new ArgumentException("Edital não encontrado.");

        // Verifica se o período do edital é válido
        if (notice.RegistrationStartDate > DateTime.UtcNow || notice.RegistrationEndDate < DateTime.UtcNow)
            throw new ArgumentException("Fora do período de inscrição no edital.");

        // Verifica se a Subárea existe
        _ = await _subAreaRepository.GetById(project.SubAreaId)
            ?? throw new ArgumentException("Subárea não encontrada.");

        // Verifica se o Tipo de Programa existe
        _ = await _programTypeRepository.GetById(project.ProgramTypeId)
            ?? throw new ArgumentException("Tipo de Programa não encontrado.");

        // Verifica se o Professor existe
        _ = await _professorRepository.GetById(project.ProfessorId)
            ?? throw new ArgumentException("Professor não encontrado.");

        // Caso tenha sido informado algum aluno no processo de abertura do projeto
        if (project.StudentId.HasValue)
        {
            // Verifica se o aluno existe
            var student = await _studentRepository.GetById(project.StudentId)
                ?? throw new ArgumentException("Aluno não encontrado.");

            // Verifica se o aluno já está em um projeto
            var studentProjects = await _projectRepository.GetStudentProjects(0, 1, student.Id);
            if (studentProjects.Any())
                throw new ArgumentException("Aluno já está em um projeto.");
        }

        // TODO: Inserir criação das atividades (ProjectActivities)

        // Cria o projeto
        project = await _projectRepository.Create(project);

        // Mapeia o projeto para o retorno e retorna
        return _mapper.Map<ResumedReadProjectOutput>(project);
    }
}