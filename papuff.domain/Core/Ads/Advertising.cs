using papuff.domain.Core.Base;

namespace papuff.domain.Core.Ads {
    public class Advertising : EntityBase {

        // for entity
        protected Advertising() { }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ContentUri { get; set; }
        public string RedirectUri { get; set; }
    }
}
