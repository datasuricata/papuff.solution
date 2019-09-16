using papuff.domain.Core.Enums;
using papuff.domain.Core.Generals;

namespace papuff.domain.Arguments.Users {
    public class DocumentResponse {
        public string Id { get; set; }
        public string Value { get; set; }
        public string ImageUri { get; set; }
        public bool Aproved { get; set; }
        public DocumentType Type { get; set; }
        public string UserId { get; set; }

        public static explicit operator DocumentResponse(Document v) {
            return v == null ? null : new DocumentResponse {
                Id = v.Id, Aproved = v.Aproved,
                ImageUri = v.ImageUri, Type = v.Type,
                UserId = v.UserId, Value = v.Value,
            };
        }
    }
}