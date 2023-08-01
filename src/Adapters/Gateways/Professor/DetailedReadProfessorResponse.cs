using Adapters.Gateways.Base;
using Adapters.Gateways.User;

namespace Adapters.Gateways.Professor
{
    public class DetailedReadProfessorResponse : IResponse
    {
        public Guid? Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public UserReadResponse? User { get; set; }

        #region Required Properties
        public string? SIAPEEnrollment { get; set; }
        public long IdentifyLattes { get; set; }
        #endregion Required Properties
    }
}