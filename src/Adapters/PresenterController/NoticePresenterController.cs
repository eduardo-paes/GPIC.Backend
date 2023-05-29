using Adapters.Gateways.Base;
using Adapters.Gateways.Notice;
using Adapters.Interfaces;
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
        private readonly IGetNotices _getNoticees;
        private readonly IGetNoticeById _getNoticeById;

        public NoticePresenterController(ICreateNotice createNotice,
            IUpdateNotice updateNotice,
            IDeleteNotice deleteNotice,
            IGetNotices getNoticees,
            IGetNoticeById getNoticeById)
        {
            _createNotice = createNotice;
            _updateNotice = updateNotice;
            _deleteNotice = deleteNotice;
            _getNoticees = getNoticees;
            _getNoticeById = getNoticeById;
        }
        #endregion

        public async Task<IResponse?> Create(IRequest request) => await _createNotice.Execute((request as CreateNoticeInput)!) as DetailedReadNoticeResponse;
        public async Task<IResponse?> Delete(Guid? id) => await _deleteNotice.Execute(id) as DetailedReadNoticeResponse;
        public async Task<IEnumerable<IResponse>?> GetAll(int skip, int take) => await _getNoticees.Execute(skip, take) as IEnumerable<ResumedReadNoticeResponse>;
        public async Task<IResponse?> GetById(Guid? id) => await _getNoticeById.Execute(id) as DetailedReadNoticeResponse;
        public async Task<IResponse?> Update(Guid? id, IRequest request) => await _updateNotice.Execute(id, (request as UpdateNoticeInput)!) as DetailedReadNoticeResponse;
    }
}