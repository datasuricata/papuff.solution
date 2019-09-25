using papuff.domain.Core.Base;

namespace papuff.domain.Core.Wallets {
    public class Receipt : EntityBase {

        public string Agency { get; private set; }
        public string Account { get; private set; }
        public int DateDue { get; private set; }

        public string WalletId { get; set; }
        public Wallet Wallet { get; set; }

        protected Receipt() {
        }

        public Receipt(string agency, string account, int dateDue, string walletId) {
            Agency = agency;
            Account = account;
            DateDue = dateDue;
            WalletId = walletId;
        }

        public void Update(string agency, string account, int dateDue) {
            Agency = agency ?? Agency;
            Account = account ?? Account;
            DateDue = dateDue == 0 ? DateDue : dateDue;
        }
    }
}