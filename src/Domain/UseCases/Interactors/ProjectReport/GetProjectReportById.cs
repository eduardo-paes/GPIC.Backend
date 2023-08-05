using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.ProjectReport;
using Domain.UseCases.Ports.ProjectReport;
using Domain.Validation;

namespace Domain.UseCases.Interactors.ProjectReport
{
    public class GetProjectReportById : IGetProjectReportById
    {
        #region Global Scope
        private readonly IProjectReportRepository _repository;
        private readonly IMapper _mapper;
        public GetProjectReportById(IProjectReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadProjectReportOutput> ExecuteAsync(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));

            Entities.ProjectReport? entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<DetailedReadProjectReportOutput>(entity);
        }
    }
}