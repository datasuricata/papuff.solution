using papuff.domain.Core.Base;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Users;

namespace papuff.domain.Core.Generals {
    public class Document : EntityBase {

        public string Value { get; set; }
        public string ImageUri { get; set; }
        public bool Aproved { get; set; }
        public DocumentType Type { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public Document(string value, string imageUri, DocumentType type, string userId) {
            Value = value;
            ImageUri = imageUri;
            Type = type;
            UserId = userId;
        }

        protected Document() { }

        public void Update(string value, string imageUri, DocumentType type) {
            Value = value ?? Value;
            ImageUri = imageUri ?? ImageUri;
            Type = type;
        }

        public void PadLock() {
            Aproved = !Aproved;
        }
    }
}
