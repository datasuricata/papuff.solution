using papuff.domain.Core.Base;
using papuff.domain.Core.Companies;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Users;
using System;

namespace papuff.domain.Core.Generals {
    public class General : EntityBase {

        public General(DateTime birthDate, string name, string description, CurrentStage stage, string userId) {
            BirthDate = birthDate;
            Name = name;
            Description = description;
            Stage = stage;
            UserId = userId;
        }

        public void Update(DateTime birthDate, string name, string description, CurrentStage stage) {
            BirthDate = birthDate;
            Name = name;
            Description = description;
            Stage = stage;
        }

        protected General() { }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public CurrentStage Stage { get; private set; }
        public DateTime BirthDate { get; private set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
