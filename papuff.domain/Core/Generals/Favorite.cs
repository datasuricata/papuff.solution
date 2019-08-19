using papuff.domain.Core.Base;
using papuff.domain.Core.Sieges;
using papuff.domain.Core.Users;

namespace papuff.domain.Core.Generals {
    public class Favorite : EntityBase {

        // for entity
        protected Favorite() { }

        public bool Like { get; set; }
        public decimal Rate { get; set; }

        public string SiegeId { get; set; }
        public Siege Siege { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
