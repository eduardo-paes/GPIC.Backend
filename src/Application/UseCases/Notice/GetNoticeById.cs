using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Notice;
using Application.Ports.Notice;
using Application.Validation;

namespace Application.UseCases.Notice
{
    public class GetNoticeById : IGetNoticeById
    {
        #region Global Scope
        private readonly INoticeRepository _repository;
        private readonly IMapper _mapper;
        public GetNoticeById(INoticeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadNoticeOutput> ExecuteAsync(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<DetailedReadNoticeOutput>(entity);
        }
    }
}