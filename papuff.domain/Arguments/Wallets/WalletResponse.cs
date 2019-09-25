using papuff.domain.Arguments.Wallets;
using papuff.domain.Core.Wallets;
using System.Collections.Generic;

namespace papuff.domain.Arguments.Users {
    public class WalletResponse {
        public string Id { get; set; }
        public string UserId { get; set; }

        public List<PaymentResponse> Payments = new List<PaymentResponse>();
        public ReceiptResponse Receipt { get; set; }

        public static explicit operator WalletResponse(Wallet v) {
            return v == null ? null : new WalletResponse {
                Id = v.Id,
                UserId = v.UserId,
                Receipt = (ReceiptResponse)v.Receipt,
                Payments = v.Payments.ConvertAll(e => (PaymentResponse)e),
            };
        }
    }
}