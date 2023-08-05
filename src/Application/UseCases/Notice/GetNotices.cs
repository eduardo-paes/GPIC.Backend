using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Notice;
using Application.Ports.Notice;

namespace Application.UseCases.Notice
{
    public class GetNotices : IGetNotices
    {
        #region Global Scope
        private readonly INoticeRepository _repository;
        private readonly IMapper _mapper;
        public GetNotices(INoticeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IEnumerable<ResumedReadNoticeOutput>> ExecuteAsync(int skip, int take)
        {
            var entities = await _repository.GetAllAsync(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadNoticeOutput>>(entities);
        }
    }
}