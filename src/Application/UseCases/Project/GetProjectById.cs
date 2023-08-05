using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Project;
using Application.Ports.Project;
using Application.Validation;

namespace Application.UseCases.Project
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
            var project = await _projectRepository.GetByIdAsync(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.Project));

            // Mapeia entidade para output e retorna
            return _mapper.Map<DetailedReadProjectOutput>(project);
        }
    }
}