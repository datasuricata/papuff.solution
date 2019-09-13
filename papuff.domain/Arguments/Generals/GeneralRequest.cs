using Newtonsoft.Json;
using System;

namespace papuff.domain.Arguments.Generals {
    public class GeneralRequest {

        public string Id { get; set; }
        public DateTime BirthDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}
