using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Project;
using Domain.UseCases.Ports.Project;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Project
{
    public class GetProjectById : IGetProjectById
    {
        #region Global Scope
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public GetProjectById(IProjectRepository projectRepository,
            IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadProjectOutput> ExecuteAsync(Guid? id)
        {
            // Busca projeto pelo Id informado
            Entities.Project project = await _projectRepository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Project));

            // Mapeia entidade para output e retorna
            return _mapper.Map<DetailedReadProjectOutput>(project);
        }
    }
}