using Newtonsoft.Json;

namespace papuff.domain.Arguments.Companies {
    public class AdsRequest {

        public string SiegeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ContentUri { get; set; }
        public string RedirectUri { get; set; }

        [JsonIgnore]
        public string OwnerId { get; set; }
    }
}
