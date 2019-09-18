using papuff.domain.Core.Base;
using papuff.domain.Core.Users;
using System;

namespace papuff.domain.Core.Companies {
    public class Company : EntityBase {

        protected Company() { }

        public Company(string name, string email, string siteUri, string cnpj, string tell, string registration, DateTime openingDate, string userId) {
            Name = name;
            Email = email;
            SiteUri = siteUri;
            CNPJ = cnpj;
            Tell = tell;
            UserId = userId;
            Registration = registration;
            OpeningDate = openingDate;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string SiteUri { get; private set; }
        public string CNPJ { get; private set; }
        public string Tell { get; private set; }
        public string Registration { get; private set; }
        public DateTime OpeningDate { get; private set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public Address Address { get; set; }
    }
}
