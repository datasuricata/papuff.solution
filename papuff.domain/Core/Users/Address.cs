using papuff.domain.Core.Base;
using papuff.domain.Core.Enums;

namespace papuff.domain.Core.Users {
    public class Address : EntityBase {

        public Address(BuildingType building, int number, int complement, string addressLine, string district, string city, string stateProvince, string country, string postalCode, string userId) {
            Building = building;
            Number = number;
            Complement = complement;
            AddressLine = addressLine;
            District = district;
            City = city;
            StateProvince = stateProvince;
            Country = country;
            PostalCode = postalCode;
            UserId = userId;
        }

        public void Update(BuildingType building, int number, int complement, string addressLine, string district, string city, string stateProvince, string country, string postalCode) {
            Building = building;
            Number = number;
            Complement = complement;
            AddressLine = addressLine;
            District = district;
            City = city;
            StateProvince = stateProvince;
            Country = country;
            PostalCode = postalCode;
        }

        protected Address() { }

        public BuildingType Building { get; set; }

        public int Number { get; set; }
        public int Complement { get; set; }

        public string AddressLine { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        //public string CompanyId { get; set; }
        //public Company Company { get; set; }
    }
}
