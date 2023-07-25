using AutoMapper;
using Domain.Contracts.Project;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases.Project;
using Domain.Validation;

namespace Domain.UseCases.Project;
public class UpdateProject : IUpdateProject
{
    #region Global Scope
    private readonly IProjectRepository _projectRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IProfessorRepository _professorRepository;
    private readonly INoticeRepository _noticeRepository;
    private readonly ISubAreaRepository _subAreaRepository;
    private readonly IProgramTypeRepository _programTypeRepository;
    private readonly IMapper _mapper;
    public UpdateProject(IProjectRepository projectRepository,
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

    public async Task<ResumedReadProjectOutput> Execute(Guid? id, UpdateProjectInput input)
    {
        // Verifica se o id foi informado
        UseCaseException.NotInformedParam(id is null, nameof(id));

        // Verifica se o projeto existe
        var project = await _projectRepository.GetById(id)
            ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Project));

        // Verifica se o projeto está aberto
        if (project!.Status == EProjectStatus.Opened)
        {
            // Verifica se a nova Subárea existe
            if (input.SubAreaId != project.SubAreaId)
            {
                _ = await _subAreaRepository.GetById(input.SubAreaId)
                    ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.SubArea));
            }

            // Verifica se o novo Tipo de Programa existe
            if (input.ProgramTypeId != project.ProgramTypeId)
            {
                _ = await _programTypeRepository.GetById(input.ProgramTypeId)
                    ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.ProgramType));
            }

            // Caso tenha sido informado algum aluno no processo de abertura do projeto
            if (input.StudentId.HasValue && input.StudentId != project.StudentId)
            {
                // Verifica se o aluno existe
                var student = await _studentRepository.GetById(input.StudentId)
                    ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Student));

                // Verifica se o aluno já está em um projeto
                var studentProjects = await _projectRepository.GetStudentProjects(0, 1, student.Id);
                UseCaseException.BusinessRuleViolation(studentProjects.Any(), "Student is already on a project.");
            }

            // TODO: Inserir atualização das atividades (ProjectActivities)

            // Atualiza campos permitidos
            project.Title = input.Title;
            project.KeyWord1 = input.KeyWord1;
            project.KeyWord2 = input.KeyWord2;
            project.KeyWord3 = input.KeyWord3;
            project.IsScholarshipCandidate = input.IsScholarshipCandidate;
            project.Objective = input.Objective;
            project.Methodology = input.Methodology;
            project.ExpectedResults = input.ExpectedResults;
            project.ActivitiesExecutionSchedule = input.ActivitiesExecutionSchedule;
            project.ProgramTypeId = input.ProgramTypeId;
            project.StudentId = input.StudentId;
            project.SubAreaId = input.SubAreaId;

            // Atualiza o projeto
            await _projectRepository.Update(project);

            // Mapeia o projeto para o retorno e retorna
            return _mapper.Map<ResumedReadProjectOutput>(project);
        }
        else
        {
            throw UseCaseException.BusinessRuleViolation("O projeto não está em um estágio que permita mudanças.");
        }
    }
}