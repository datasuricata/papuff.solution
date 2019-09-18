using papuff.domain.Core.Base;
using papuff.domain.Core.Enums;

namespace papuff.domain.Core.Wallets {
    public class Receipt : EntityBase {

        public string Agency { get; private set; }
        public string Account { get; private set; }

        public int DateDue { get; private set; }
        public PaymentType Type { get; private set; }

        public string WalletId { get; set; }
        public Wallet Wallet { get; set; }

        protected Receipt() {
        }

        public Receipt(string agency, string account, int dateDue, PaymentType type, string walletId) {
            Agency = agency;
            Account = account;
            DateDue = dateDue;
            Type = type;
            WalletId = walletId;
        }
    }
}