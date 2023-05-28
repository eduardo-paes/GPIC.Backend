using AutoMapper;
using Domain.Contracts.Project;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;

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
        var entity = _mapper.Map<Entities.Project>(input);

        // Verifica se Edital existe
        var notice = await _noticeRepository.GetById(input.NoticeId)
            ?? throw new ArgumentException("Edital não encontrado.");

        // Verifica se o período do edital é válido
        if (notice.StartDate > DateTime.Now || notice.FinalDate < DateTime.Now)
            throw new ArgumentException("Fora do período de inscrição no edital.");

        // Verifica se a Subárea existe
        _ = await _subAreaRepository.GetById(input.SubAreaId)
            ?? throw new ArgumentException("Subárea não encontrada.");

        // Verifica se o Tipo de Programa existe
        _ = await _programTypeRepository.GetById(input.ProgramTypeId)
            ?? throw new ArgumentException("Tipo de Programa não encontrado.");

        // Verifica se o Professor existe
        _ = await _professorRepository.GetById(input.ProfessorId)
            ?? throw new ArgumentException("Professor não encontrado.");

        // Caso tenha sido informado algum aluno no processo de abertura do projeto
        if (input.StudentId.HasValue)
        {
            // Verifica se o aluno existe
            var student = await _studentRepository.GetById(input.StudentId)
                ?? throw new ArgumentException("Aluno não encontrado.");

            // Verifica se o aluno já está em um projeto
            var studentProjects = await _projectRepository.GetStudentProjects(0, 1, student.Id);
            if (studentProjects.Any())
                throw new ArgumentException("Aluno já está em um projeto.");
        }

        // Atualiza o status do projeto
        entity.Status = EProjectStatus.Opened;
        entity.StatusDescription = EProjectStatus.Opened.GetDescription();

        // Cria o projeto
        var project = await _projectRepository.Create(entity);

        // Mapeia o projeto para o retorno e retorna
        return _mapper.Map<ResumedReadProjectOutput>(project);
    }
}