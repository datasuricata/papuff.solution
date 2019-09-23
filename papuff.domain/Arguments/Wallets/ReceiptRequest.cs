using papuff.domain.Core.Enums;

namespace papuff.domain.Arguments.Users {
    public class ReceiptRequest {
        public string Agency { get; set; }
        public string Account { get; set; }
        public int DateDue { get; set; }
        public PaymentType Type { get; set; }
    }
}