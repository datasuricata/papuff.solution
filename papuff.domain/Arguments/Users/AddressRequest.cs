using Newtonsoft.Json;
using papuff.domain.Core.Enums;

namespace papuff.domain.Arguments.Users {
    public class AddressRequest {

        public string Id { get; set; }
        public string CompanyId { get; set; }

        public BuildingType Building { get; set; }

        public int Number { get; set; }
        public int Complement { get; set; }

        public string AddressLine { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}
