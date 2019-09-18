using papuff.domain.Core.Base;
using papuff.domain.Core.Users;
using System.Collections.Generic;

namespace papuff.domain.Core.Wallets {
    public class Wallet : EntityBase {

        public List<Payment> Payments = new List<Payment>();

        public Receipt Receipt { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        protected Wallet() {
        }

        public Wallet(string userId) {
            UserId = userId;
        }
    }
}