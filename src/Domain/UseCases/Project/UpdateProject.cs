using AutoMapper;
using Domain.Contracts.Project;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;
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

    public async Task<ResumedReadProjectOutput> Execute(UpdateProjectInput input)
    {
        // Verifica se o projeto existe
        var project = await _projectRepository.GetById(input.Id)
            ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Project));

        // Verifica se o projeto está aberto
        if (project!.Status == EProjectStatus.Opened)
        {
            // Mapeia input para entidade e realiza validação dos campos informados
            var entity = _mapper.Map<Entities.Project>(input);

            // Verifica se a nova Subárea existe
            if (input.SubAreaId != entity.SubAreaId)
            {
                _ = await _subAreaRepository.GetById(input.SubAreaId)
                    ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.SubArea));
            }

            // Verifica se o novo Tipo de Programa existe
            if (input.ProgramTypeId != entity.ProgramTypeId)
            {
                _ = await _programTypeRepository.GetById(input.ProgramTypeId)
                    ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.ProgramType));
            }

            // Caso tenha sido informado algum aluno no processo de abertura do projeto
            if (input.StudentId.HasValue && input.StudentId != entity.StudentId)
            {
                // Verifica se o aluno existe
                var student = await _studentRepository.GetById(input.StudentId)
                    ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Student));

                // Verifica se o aluno já está em um projeto
                var studentProjects = await _projectRepository.GetStudentProjects(0, 1, student.Id);
                if (studentProjects.Any())
                    throw UseCaseException.BusinessRuleViolation("Student is already on a project.");
            }

            // Atualiza campos permitidos
            project.Title = entity.Title;
            project.KeyWord1 = entity.KeyWord1;
            project.KeyWord2 = entity.KeyWord2;
            project.KeyWord3 = entity.KeyWord3;
            project.IsScholarshipCandidate = entity.IsScholarshipCandidate;
            project.Objective = entity.Objective;
            project.Methodology = entity.Methodology;
            project.ExpectedResults = entity.ExpectedResults;
            project.ActivitiesExecutionSchedule = entity.ActivitiesExecutionSchedule;
            project.WorkType1 = entity.WorkType1;
            project.WorkType2 = entity.WorkType2;
            project.IndexedConferenceProceedings = entity.IndexedConferenceProceedings;
            project.NotIndexedConferenceProceedings = entity.NotIndexedConferenceProceedings;
            project.CompletedBook = entity.CompletedBook;
            project.OrganizedBook = entity.OrganizedBook;
            project.BookChapters = entity.BookChapters;
            project.BookTranslations = entity.BookTranslations;
            project.ParticipationEditorialCommittees = entity.ParticipationEditorialCommittees;
            project.FullComposerSoloOrchestraAllTracks = entity.FullComposerSoloOrchestraAllTracks;
            project.FullComposerSoloOrchestraCompilation = entity.FullComposerSoloOrchestraCompilation;
            project.ChamberOrchestraInterpretation = entity.ChamberOrchestraInterpretation;
            project.IndividualAndCollectiveArtPerformances = entity.IndividualAndCollectiveArtPerformances;
            project.ScientificCulturalArtisticCollectionsCuratorship = entity.ScientificCulturalArtisticCollectionsCuratorship;
            project.PatentLetter = entity.PatentLetter;
            project.PatentDeposit = entity.PatentDeposit;
            project.SoftwareRegistration = entity.SoftwareRegistration;
            project.ProgramTypeId = entity.ProgramTypeId;
            project.StudentId = entity.StudentId;
            project.SubAreaId = entity.SubAreaId;

            // Atualiza o projeto
            project = await _projectRepository.Update(entity);

            // Mapeia o projeto para o retorno e retorna
            return _mapper.Map<ResumedReadProjectOutput>(project);
        }
        else
        {
            throw UseCaseException.BusinessRuleViolation("The project is not at a stage that allows for changes.");
        }
    }
}