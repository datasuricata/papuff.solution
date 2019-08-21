using papuff.domain.Core.Ads;
using papuff.domain.Core.Sieges;
using papuff.domain.Core.Users;
using papuff.domain.Interfaces.Services.Swap;
using System.Collections.Generic;
using System.Linq;

namespace papuff.services.Services.Swap {
    public sealed class SwapSiege : ISwapSiege {

        private static readonly List<Siege> Sieges = new List<Siege>();
        private static readonly object _lock = new object();

        public Siege GetById(string id) {
            return Sieges.FirstOrDefault(x => x.Id == id);
        }

        public List<Siege> ListSieges() {
            return Sieges;
        }

        public void Add(Siege siege) {
            lock (_lock) {
                Sieges.Add(siege);
            }
        }

        public void Close(string id) {

            var siege = GetById(id);

            lock (_lock) {
                siege.End();
            }
        }

        public void ReceiveUser(string id, User user) {

            var siege = GetById(id);

            lock (_lock) {
                siege.Users.Add(user);
            }
        }

        public void RemoveUser(string id, User user) {

            var siege = GetById(id);

            lock (_lock) {
                siege.Users.Remove(user);
            }
        }


        public void PushAds(string id, Advertising advertising) {

            var siege = GetById(id);

            lock (_lock) {
                siege.Advertising = advertising;
                siege.PushAds(); 
            }
        }
    }
}
