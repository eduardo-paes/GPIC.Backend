using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.ProjectReport;
using Application.Ports.ProjectReport;
using Application.Validation;

namespace Application.UseCases.ProjectReport
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
            Domain.Entities.ProjectReport? entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<DetailedReadProjectReportOutput>(entity);
        }
    }
}