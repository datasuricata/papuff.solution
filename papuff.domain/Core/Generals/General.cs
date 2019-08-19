using papuff.domain.Core.Base;
using papuff.domain.Core.Companies;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Users;

namespace papuff.domain.Core.Generals {
    public class General : EntityBase {

        // for entity
        //protected General() { }

        public string BirthDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CurrentStage Stage { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
