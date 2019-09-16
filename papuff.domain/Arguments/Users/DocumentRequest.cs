using Newtonsoft.Json;
using papuff.domain.Core.Enums;

namespace papuff.domain.Arguments.Users {
    public class DocumentRequest {

        public string Id { get; set; }
        public string Value { get; set; }
        public string ImageUri { get; set; }
        public DocumentType Type { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}