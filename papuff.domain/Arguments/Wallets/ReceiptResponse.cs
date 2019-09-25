using papuff.domain.Core.Wallets;

namespace papuff.domain.Arguments.Wallets {
    public class ReceiptResponse {
        public string Id { get; set; }
        public string WalletId { get; set; }
        public string Agency { get; set; }
        public string Account { get; set; }
        public int DateDue { get; set; }

        public static explicit operator ReceiptResponse(Receipt v) {
            return v == null ? null : new ReceiptResponse {
                Id = v.Id, WalletId = v.WalletId, Account = v.Account,
                DateDue = v.DateDue, Agency = v.Agency,
            };
        }
    }
}