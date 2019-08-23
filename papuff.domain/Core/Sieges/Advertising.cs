using papuff.domain.Core.Base;

namespace papuff.domain.Core.Sieges {
    public class Advertising : EntityBase {

        public Advertising(string title, string description, string contentUri, string redirectUri) {
            Title = title;
            Description = description;
            ContentUri = contentUri;
            RedirectUri = redirectUri;
        }

        // for entity
        protected Advertising() { }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ContentUri { get; private set; }
        public string RedirectUri { get; private set; }
    }
}
