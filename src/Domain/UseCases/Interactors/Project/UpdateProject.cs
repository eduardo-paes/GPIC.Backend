using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Project;
using Domain.UseCases.Ports.Project;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Project
{
    public class UpdateProject : IUpdateProject
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
        public UpdateProject(IProjectRepository projectRepository,
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

        public async Task<ResumedReadProjectOutput> ExecuteAsync(Guid? id, UpdateProjectInput input)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Verifica se o projeto existe
            Entities.Project project = await _projectRepository.GetByIdAsync(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Project));

            // Verifica se o edital está no período de inscrições
            if (project.Notice!.RegistrationStartDate > DateTime.UtcNow || project.Notice?.RegistrationEndDate < DateTime.UtcNow)
            {
                throw UseCaseException.BusinessRuleViolation("Fora do período de inscrição no edital.");
            }

            // Verifica se o projeto está aberto
            if (project!.Status == EProjectStatus.Opened)
            {
                // Verifica se a nova Subárea existe
                if (input.SubAreaId != project.SubAreaId)
                {
                    _ = await _subAreaRepository.GetByIdAsync(input.SubAreaId)
                        ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.SubArea));
                }

                // Verifica se o novo Tipo de Programa existe
                if (input.ProgramTypeId != project.ProgramTypeId)
                {
                    _ = await _programTypeRepository.GetByIdAsync(input.ProgramTypeId)
                        ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.ProgramType));
                }

                // Caso tenha sido informado algum aluno no processo de abertura do projeto
                if (input.StudentId.HasValue && input.StudentId != project.StudentId)
                {
                    // Verifica se o aluno existe
                    Entities.Student student = await _studentRepository.GetByIdAsync(input.StudentId)
                        ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Student));

                    // Verifica se o aluno já está em um projeto
                    IEnumerable<Entities.Project> studentProjects = await _projectRepository.GetStudentProjectsAsync(0, 1, student.Id);
                    UseCaseException.BusinessRuleViolation(studentProjects.Any(), "Student is already on a project.");
                }

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

                // Verifica se foram informadas atividades
                if (input.Activities?.Any() != true)
                {
                    throw UseCaseException.BusinessRuleViolation("Atividades não informadas.");
                }

                // Obtém atividades do Edital
                IList<Entities.ActivityType> noticeActivities = await _activityTypeRepository.GetByNoticeIdAsync(project.Notice!.Id);

                // Obtém atividades do projeto
                IList<Entities.ProjectActivity> projectActivities = await _projectActivityRepository.GetByProjectIdAsync(project.Id);

                // Valida se todas as atividades do projeto foram informadas corretamente
                List<Entities.ProjectActivity> updateProjectActivities = new();
                foreach (Entities.ActivityType activityType in noticeActivities)
                {
                    // Verifica se as atividades que o professor informou existem no edital 
                    // e se todas as atividades do edital foram informadas.
                    foreach (Entities.Activity activity in activityType.Activities!)
                    {
                        // Verifica se professor informou valor para essa atividade do edital
                        Ports.ProjectActivity.UpdateProjectActivityInput inputActivity = input.Activities!.FirstOrDefault(x => x.ActivityId == activity.Id)
                            ?? throw UseCaseException.BusinessRuleViolation($"Não foi informado valor para a atividade {activity.Name}.");

                        // Obtém atividade do projeto
                        Entities.ProjectActivity? updateProjectActivity = projectActivities.FirstOrDefault(x => x.ActivityId == activity.Id);

                        // Atualiza valores da entidade
                        updateProjectActivity!.InformedActivities = inputActivity.InformedActivities;

                        // Atualiza atividade do projeto no banco de dados
                        _ = await _projectActivityRepository.UpdateAsync(updateProjectActivity);
                    }
                }

                // Atualiza o projeto
                _ = await _projectRepository.UpdateAsync(project);

                // Atualiza atividades do projeto no banco
                foreach (Entities.ProjectActivity projectActivity in updateProjectActivities)
                {
                    _ = await _projectActivityRepository.UpdateAsync(projectActivity);
                }

                // Mapeia o projeto para o retorno e retorna
                return _mapper.Map<ResumedReadProjectOutput>(project);
            }

            throw UseCaseException.BusinessRuleViolation("O projeto não está em um estágio que permita mudanças.");
        }
    }
}