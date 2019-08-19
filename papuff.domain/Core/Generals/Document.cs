using papuff.domain.Core.Base;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Users;

namespace papuff.domain.Core.Generals {
    public class Document : EntityBase {

        // for entity 
        protected Document() { }

        public string Value { get; set; }
        public string ImageUri { get; set; }
        public DocumentType Type { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        //public Document(string value, string imageUri, DocumentType type) {
        //    Value = value;
        //    ImageUri = imageUri;
        //    Type = type;
        //}

        //public static Document Register(string value, string imageUri, DocumentType type, string userId) {
        //    return new Document(value, imageUri, type) {
        //        UserId = userId,
        //    };
        //}

    }
}
