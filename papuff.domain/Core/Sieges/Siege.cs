using papuff.domain.Core.Ads;
using papuff.domain.Core.Base;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Users;
using System.Collections.Generic;

namespace papuff.domain.Core.Sieges {
    public class Siege : EntityBase {

        // for entity
        protected Siege() { }

        public VisibilityType Visibility { get; set; }
        public double Range { get; set; }

        public Advertising Advertising { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
