using papuff.domain.Arguments.Users;
using papuff.domain.Core.Companies;
using System;

namespace papuff.domain.Arguments.Companies {
    public class CompanyResponse {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string SiteUri { get; set; }
        public string CNPJ { get; set; }
        public string Tell { get; set; }
        public string Registration { get; set; }
        public DateTime OpeningDate { get; set; }
        public string UserId { get; set; }
        public AddressResponse Address { get; set; }

        public static explicit operator CompanyResponse(Company v) {
            return v == null ? null : new CompanyResponse {
                Id = v.Id, CNPJ = v.CNPJ, Name = v.Name,
                Email = v.Email, OpeningDate = v.OpeningDate,
                Registration = v.Registration, SiteUri = v.SiteUri,
                Tell = v.Tell, UserId = v.UserId,
                Address = (AddressResponse)v.Address,
            };
        }
    }
}