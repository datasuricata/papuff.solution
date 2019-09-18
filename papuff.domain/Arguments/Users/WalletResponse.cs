using papuff.domain.Core.Wallets;

namespace papuff.domain.Arguments.Users {
    public class WalletResponse {
        public string Id { get; set; }
        
        public static explicit operator WalletResponse(Wallet v) {
            return v == null ? null : new WalletResponse {
                Id = v.Id,
            };
        }
    }
}