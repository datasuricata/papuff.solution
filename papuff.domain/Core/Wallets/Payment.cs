using papuff.domain.Core.Base;
using papuff.domain.Core.Enums;
using System;

namespace papuff.domain.Core.Wallets {
    public class Payment : EntityBase {

        public string Card { get; private set; }
        public DateTime Expiration { get; private set; }
        public PaymentType Type { get; private set; }
        public int Code { get; private set; }
        public int DateDue { get; private set; }
        public string Document { get; private set; }
        public bool IsDefault { get; private set; }

        public string WalletId { get; set; }
        public Wallet Wallet { get; set; }

        protected Payment() { }

        public Payment(string card, DateTime expiration, int code, int dateDue, string document, bool isDefault, PaymentType type, string walletId) {
            Code = code;
            Card = card;
            Type = type;
            Expiration = expiration;
            WalletId = walletId;
            DateDue = dateDue;
            Document = document;
            IsDefault = isDefault;
        }
    }
}