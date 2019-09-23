using papuff.domain.Core.Enums;
using System;

namespace papuff.domain.Arguments.Users {
    public class PaymentRequest {
        public DateTime Expiration { get; set; }
        public PaymentType Type { get; set; }
        public int Code { get; set; }
        public int DateDue { get; set; }
        public string Card { get; set; }
        public string Document { get; set; }
        public bool IsDefault { get; set; }
    }
}