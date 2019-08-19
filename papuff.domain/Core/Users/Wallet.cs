using papuff.domain.Core.Base;
using papuff.domain.Core.Enums;

namespace papuff.domain.Core.Users {
    public class Wallet : EntityBase {

        // for entity
        protected Wallet() { }

        public PaymentType Type { get; private set; }

        public string Agency { get; private set; }
        public string Account { get; private set; }
        public string Document { get; private set; }
        public int DateDue { get; private set; }
        public bool IsDefault { get; private set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
