using papuff.domain.Core.Base;
using papuff.domain.Core.Users;

namespace papuff.domain.Core.Sieges {
    public class Ticket : EntityBase {

        #region - attributes -

        public User User { get; set; }
        public string UserId { get; set; }

        public Siege Siege { get; set; }
        public string SiegeId { get; set; }

        public string Hash { get; set; }
        public int DateDue { get; set; }

        #endregion

        #region - ctor -

        protected Ticket() {
        }

        public Ticket(string siegeId, string hash, int dateDue) {
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