using papuff.domain.Core.Enums;
using papuff.domain.Core.Wallets;

namespace papuff.domain.Arguments.Wallets {
    public class PaymentResponse {
        public string Id { get; set; }
        public string WalletId { get; set; }
        public string Card { get; set; }
        public string Document { get; set; }
        public PaymentType Type { get; set; }
        public bool IsDefault { get; set; }

        public static explicit operator PaymentResponse(Payment v) {
            return v == null ? null : new PaymentResponse {
                Id = v.Id, WalletId = v.WalletId, Card = v.Card,
                Document = v.Document, IsDefault = v.IsDefault, Type = v.Type,
            };
        }
    }
}