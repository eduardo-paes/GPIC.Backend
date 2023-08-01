using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Project;
using Domain.UseCases.Ports.Project;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Project
{
    public class OpenProject : IOpenProject
    {
        #region Global Scope
        private readonly IProjectRepository _projectRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly INoticeRepository _noticeRepository;
        private readonly ISubAreaRepository _subAreaRepository;
        private readonly IProgramTypeRepository _programTypeRepository;
        private readonly IActivityTypeRepository _activityTypeRepository;
        private readonly IProjectActivityRepository _projectActivityRepository;
        private readonly IMapper _mapper;
        public OpenProject(IProjectRepository projectRepository,
            IStudentRepository studentRepository,
            IProfessorRepository professorRepository,
            INoticeRepository noticeRepository,
            ISubAreaRepository subAreaRepository,
            IProgramTypeRepository programTypeRepository,
            IActivityTypeRepository activityTypeRepository,
            IProjectActivityRepository projectActivityRepository,
            IMapper mapper)
        {
            _projectRepository = projectRepository;
            _studentRepository = studentRepository;
            _professorRepository = professorRepository;
            _noticeRepository = noticeRepository;
            _subAreaRepository = subAreaRepository;
            _programTypeRepository = programTypeRepository;
            _activityTypeRepository = activityTypeRepository;
            _projectActivityRepository = projectActivityRepository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<ResumedReadProjectOutput> Execute(OpenProjectInput input)
        {
            // Mapeia input para entidade e realiza validação dos campos informados
            Entities.Project project = new(
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
            Entities.Notice notice = await _noticeRepository.GetById(project.NoticeId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Notice));

            // Verifica se o período do edital é válido
            if (notice.RegistrationStartDate > DateTime.UtcNow || notice.RegistrationEndDate < DateTime.UtcNow)
            {
                throw UseCaseException.BusinessRuleViolation("Fora do período de inscrição no edital.");
            }

            // Verifica se a Subárea existe
            _ = await _subAreaRepository.GetById(project.SubAreaId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.SubArea));

            // Verifica se o Tipo de Programa existe
            _ = await _programTypeRepository.GetById(project.ProgramTypeId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.ProgramType));

            // Verifica se o Professor existe
            _ = await _professorRepository.GetById(project.ProfessorId)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Professor));

            // Caso tenha sido informado algum aluno no processo de abertura do projeto
            if (project.StudentId.HasValue)
            {
                // Verifica se o aluno existe
                Entities.Student student = await _studentRepository.GetById(project.StudentId)
                    ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Student));

                // Verifica se o aluno já está em um projeto
                IEnumerable<Entities.Project> studentProjects = await _projectRepository.GetStudentProjects(0, 1, student.Id);
                if (studentProjects.Any())
                {
                    throw UseCaseException.BusinessRuleViolation("Aluno já está em um projeto.");
                }
            }

            // Verifica se foram informadas atividades
            if (input.Activities?.Any() != true)
            {
                throw UseCaseException.BusinessRuleViolation("Atividades não informadas.");
            }

            // Obtém atividades do Edital
            IList<Entities.ActivityType> noticeActivities = await _activityTypeRepository.GetByNoticeId(notice.Id);

            // Valida se todas as atividades do projeto foram informadas corretamente
            List<Entities.ProjectActivity> newProjectActivities = new();
            foreach (Entities.ActivityType activityType in noticeActivities)
            {
                // Verifica se as atividades que o professor informou existem no edital 
                // e se todas as atividades do edital foram informadas.
                foreach (Entities.Activity activity in activityType.Activities!)
                {
                    // Verifica se professor informou valor para essa atividade do edital
                    Ports.ProjectActivity.CreateProjectActivityInput inputActivity = input.Activities!.FirstOrDefault(x => x.ActivityId == activity.Id)
                        ?? throw UseCaseException.BusinessRuleViolation($"Não foi informado valor para a atividade {activity.Name}.");

                    // Adiciona atividade do projeto na lista para ser criada posteriormente
                    newProjectActivities.Add(new Entities.ProjectActivity(
                        Guid.Empty, // Id do projeto será gerado na etapa seguinte
                        inputActivity.ActivityId,
                        inputActivity.InformedActivities,
                        0));
                }
            }

            // Cria o projeto
            project = await _projectRepository.Create(project);

            // Cria as atividades do projeto
            foreach (Entities.ProjectActivity projectActivity in newProjectActivities)
            {
                projectActivity.ProjectId = project.Id;
                _ = await _projectActivityRepository.Create(projectActivity);
            }

            // Mapeia o projeto para o retorno e retorna
            return _mapper.Map<ResumedReadProjectOutput>(project);
        }
    }
}