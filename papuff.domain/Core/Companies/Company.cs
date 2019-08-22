using papuff.domain.Core.Base;
using papuff.domain.Core.Users;

namespace papuff.domain.Core.Companies {
    public class Company : EntityBase {

        // for entity
        protected Company() { }

        public Address Address { get; set; }
    }
}
