using Adapters.Gateways.Base;
using Adapters.Gateways.Notice;
using Adapters.Interfaces;
using AutoMapper;
using Domain.Contracts.Activity;
using Domain.Contracts.Notice;
using Domain.Interfaces.UseCases;

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
        #endregion

        public async Task<IResponse> Create(IRequest request)
        {
            var input = _mapper.Map<CreateNoticeInput>(request as CreateNoticeRequest);
            var result = await _createNotice.Execute(input);
            return _mapper.Map<DetailedReadNoticeResponse>(result);
        }

        public async Task<IResponse> Delete(Guid? id)
        {
            var result = await _deleteNotice.Execute(id);
            return _mapper.Map<DetailedReadNoticeResponse>(result);
        }

        public async Task<IEnumerable<IResponse>> GetAll(int skip, int take)
        {
            var result = await _getNotices.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadNoticeResponse>>(result);
        }

        public async Task<IResponse> GetById(Guid? id)
        {
            var result = await _getNoticeById.Execute(id);
            return _mapper.Map<DetailedReadNoticeResponse>(result);
        }

        public async Task<IResponse> Update(Guid? id, IRequest request)
        {
            var input = _mapper.Map<UpdateNoticeInput>(request as UpdateNoticeRequest);
            var result = await _updateNotice.Execute(id, input);
            return _mapper.Map<DetailedReadNoticeResponse>(result);
        }
    }
}