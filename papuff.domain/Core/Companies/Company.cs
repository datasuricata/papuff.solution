using papuff.domain.Core.Base;
using papuff.domain.Core.Generals;
using papuff.domain.Core.Users;
using System.Collections.Generic;

namespace papuff.domain.Core.Companies {
    public class Company : EntityBase {

        // for entity
        protected Company() { }

        public General General { get; set; }
        public Address Address { get; set; }
        public List<Document> Documents { get; set; } = new List<Document>();
    }
}
