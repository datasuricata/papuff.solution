using Newtonsoft.Json;
using papuff.domain.Core.Enums;

namespace papuff.domain.Arguments.Users {
    public class WalletRequest {

        public string Id { get; set; }
        public PaymentType Type { get; set; }
        public string Agency { get; set; }
        public string Account { get; set; }
        public string Document { get; set; }
        public int DateDue { get; set; }
        public bool IsDefault { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}
