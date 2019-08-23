using Newtonsoft.Json;
using System;

namespace papuff.domain.Arguments.Companies {
    public class CompanyRequest {

        public string Name { get; set; }
        public string Email { get; set; }
        public string SiteUri { get; set; }
        public string CNPJ { get; set; }
        public string Tell { get; set; }
        public string Registration { get; set; }
        public DateTime OpeningDate { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}
