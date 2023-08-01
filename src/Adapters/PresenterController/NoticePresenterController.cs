using Adapters.Gateways.Base;
using Adapters.Gateways.Notice;
using Adapters.Interfaces;
using AutoMapper;
using Domain.UseCases.Interfaces.Notice;
using Domain.UseCases.Ports.Notice;

namespace Adapters.PresenterController
{
    public class NoticePresenterController : INoticePresenterController
    {
        #region Global Scope
        private readonly ICreateNotice _createNotice;
        private readonly IUpdateNotice _updateNotice;
        private readonly IDeleteNotice _deleteNotice;
        private readonly IGetNotices _getNotices;
        private readonly IGetNoticeById _getNoticeById;
        private readonly IMapper _mapper;

        public NoticePresenterController(
            ICreateNotice createNotice,
            IUpdateNotice updateNotice,
            IDeleteNotice deleteNotice,
            IGetNotices getNotices,
            IGetNoticeById getNoticeById,
            IMapper mapper)
        {
            _createNotice = createNotice;
            _updateNotice = updateNotice;
            _deleteNotice = deleteNotice;
            _getNotices = getNotices;
            _getNoticeById = getNoticeById;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IResponse> Create(IRequest request)
        {
            CreateNoticeInput input = _mapper.Map<CreateNoticeInput>(request as CreateNoticeRequest);
            DetailedReadNoticeOutput result = await _createNotice.Execute(input);
            return _mapper.Map<DetailedReadNoticeResponse>(result);
        }

        public async Task<IResponse> Delete(Guid? id)
        {
            DetailedReadNoticeOutput result = await _deleteNotice.Execute(id);
            return _mapper.Map<DetailedReadNoticeResponse>(result);
        }

        public async Task<IEnumerable<IResponse>> GetAll(int skip, int take)
        {
            IEnumerable<ResumedReadNoticeOutput> result = await _getNotices.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadNoticeResponse>>(result);
        }

        public async Task<IResponse> GetById(Guid? id)
        {
            DetailedReadNoticeOutput result = await _getNoticeById.Execute(id);
            return _mapper.Map<DetailedReadNoticeResponse>(result);
        }

        public async Task<IResponse> Update(Guid? id, IRequest request)
        {
            UpdateNoticeInput input = _mapper.Map<UpdateNoticeInput>(request as UpdateNoticeRequest);
            DetailedReadNoticeOutput result = await _updateNotice.Execute(id, input);
            return _mapper.Map<DetailedReadNoticeResponse>(result);
        }
    }
}