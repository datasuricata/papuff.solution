using papuff.domain.Core.Base;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Users;
using System.Threading;

namespace papuff.domain.Core.Sieges {
    public class Ticket : EntityBase {

        #region - attributes -

        private Timer _audit;
        private readonly object _lock = new object();

        public User User { get; set; }
        public string UserId { get; set; }

        public Siege Siege { get; set; }
        public string SiegeId { get; set; }

        public string Hash { get; set; }
        public int DateDue { get; set; }

        public TicketType Type { get; set; }

        #endregion

        #region - ctor -

        protected Ticket() {
        }

        public Ticket(string siegeId, TicketType type, string hash, int dateDue) {
            SiegeId = siegeId;
            Hash = hash ?? CustomHash(16);
            DateDue = dateDue;
        }

        #endregion

        #region - methods -

        public void ProcessTicket() => IsDeleted = true;

        public void AssignTicket(string userId) => UserId = userId;

        #endregion
    }
}