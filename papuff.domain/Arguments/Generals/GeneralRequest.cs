using Newtonsoft.Json;
using papuff.domain.Core.Enums;
using System;

namespace papuff.domain.Arguments.Generals {
    public class GeneralRequest {

        public DateTime BirthDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CurrentStage Stage { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}
