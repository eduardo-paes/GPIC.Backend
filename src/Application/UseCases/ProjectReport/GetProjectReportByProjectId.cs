using Application.Interfaces.UseCases.ProjectReport;
using Application.Ports.ProjectReport;
using Application.Validation;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Application.UseCases.ProjectReport
{
    public class GetProjectReportByProjectId : IGetProjectReportsByProjectId
    {
        #region Global Scope
        private readonly IProjectReportRepository _repository;
        private readonly IMapper _mapper;
        public GetProjectReportByProjectId(IProjectReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IList<DetailedReadProjectReportOutput>> ExecuteAsync(Guid? projectId)
        {
            UseCaseException.NotInformedParam(projectId is null, nameof(projectId));
            var entities = await _repository.GetByProjectIdAsync(projectId);
            return _mapper.Map<IList<DetailedReadProjectReportOutput>>(entities);
        }
    }
}