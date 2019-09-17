using papuff.domain.Core.Enums;
using papuff.domain.Core.Users;

namespace papuff.domain.Arguments.Users {
    public class WalletResponse {
        public string Id { get; set; }
        public PaymentType Type { get; set; }
        public string Agency { get; set; }
        public string Account { get; set; }
        public string Document { get; set; }
        public int DateDue { get; set; }
        public bool IsDefault { get; set; }
        public string UserId { get; set; }

        public static explicit operator WalletResponse(Wallet v) {
            return v == null ? null : new WalletResponse {
                Id = v.Id, Account = v.Account, Agency = v.Agency,
                DateDue = v.DateDue, Document = v.Document, IsDefault = v.IsDefault,
                Type = v.Type, UserId = v.UserId,
            };
        }
    }
}