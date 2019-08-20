using Newtonsoft.Json;
using papuff.domain.Core.Enums;
using System;

namespace papuff.domain.Arguments.Sieges {
    public class SiegeRequest {
        public VisibilityType Visibility { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }

        public double Range { get; set; }

        public DateTime Start { get; set; }
        public DateTime Available { get; set; }

        [JsonIgnore]
        public string OwnerId { get; set; }
    }
}
