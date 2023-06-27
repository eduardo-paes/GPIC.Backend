using AutoMapper;
using Domain.Contracts.Project;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases.Project;
using Domain.Validation;

namespace Domain.UseCases.Project
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
        #endregion

        public async Task<DetailedReadProjectOutput> Execute(Guid? id)
        {
            // Busca projeto pelo Id informado
            var project = await _projectRepository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Project));

            // Mapeia entidade para output e retorna
            return _mapper.Map<DetailedReadProjectOutput>(project);
        }
    }
}