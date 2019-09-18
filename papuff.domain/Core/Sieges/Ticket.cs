using papuff.domain.Core.Base;
using papuff.domain.Core.Users;

namespace papuff.domain.Core.Sieges {
    public class Ticket : EntityBase {

        public User User { get; set; }
        public string UserId { get; set; }

        public Siege Siege { get; set; }
        public string SiegeId { get; set; }

        public string Hash { get; set; }
        public int DateDue { get; set; }
    }
}