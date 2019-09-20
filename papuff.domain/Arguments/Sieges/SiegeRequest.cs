using Newtonsoft.Json;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Sieges;
using System.Collections.Generic;

namespace papuff.domain.Arguments.Sieges {
    public class SiegeRequest {
        public VisibilityType Visibility { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Range { get; set; }
        public int Seconds { get; set; }

        [JsonIgnore]
        public string OwnerId { get; set; }
    }
}
