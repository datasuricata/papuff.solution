using papuff.domain.Core.Base;
using papuff.domain.Core.Companies;
using papuff.domain.Core.Enums;

namespace papuff.domain.Core.Users {
    public class Address : EntityBase {

        public Address(BuildingType building, int number, int complement, string addressLine, string district, string city, string stateProvince, string country, string postalCode, string referenceId, bool isCompany) {
            Building = building;
            Number = number;
            Complement = complement;
            AddressLine = addressLine;
            District = district;
            City = city;
            StateProvince = stateProvince;
            Country = country;
            PostalCode = postalCode;

            if(isCompany)
                CompanyId = referenceId;
            else
                UserId = referenceId;
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

        public BuildingType Building { get; private set; }

        public int Number { get; private set; }
        public int Complement { get; private set; }

        public string AddressLine { get; private set; }
        public string District { get; private set; }
        public string City { get; private set; }
        public string StateProvince { get; private set; }
        public string Country { get; private set; }
        public string PostalCode { get; private set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
