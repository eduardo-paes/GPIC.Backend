using Domain.Contracts.Notice;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
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
        #endregion

        public async Task<DetailedReadNoticeOutput> Execute(Guid? id)
        {
            UseCaseException.NotInformedParam(id is null, nameof(id));

            var entity = await _repository.GetById(id);
            return _mapper.Map<DetailedReadNoticeOutput>(entity);
        }
    }
}