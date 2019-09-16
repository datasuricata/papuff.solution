using papuff.domain.Core.Enums;
using papuff.domain.Core.Users;

namespace papuff.domain.Arguments.Users {
    public class AddressResponse {
        public string Id { get; set; }
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
        public string CompanyId { get; set; }

        public static explicit operator AddressResponse(Address v) {
            return v == null ? null : new AddressResponse {
                Id = v.Id, AddressLine = v.AddressLine,
                Building = v.Building, City = v.City,
                CompanyId = v.CompanyId, Complement = v.Complement,
                Country = v.Country, District = v.District,
                Number = v.Number, PostalCode = v.PostalCode,
                StateProvince = v.StateProvince, UserId = v.UserId,
            };
        }
    }
}