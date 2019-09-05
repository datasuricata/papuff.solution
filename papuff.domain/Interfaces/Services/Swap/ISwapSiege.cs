using papuff.domain.Core.Sieges;
using papuff.domain.Core.Users;
using System.Collections.Generic;

namespace papuff.domain.Interfaces.Services.Swap {
    public interface ISwapSiege {

        Siege GetById(string id);
        IEnumerable<Siege> ListSieges();
        IEnumerable<Siege> ListAvaiables();
        IEnumerable<Siege> CheckIn(IEnumerable<Siege> sieges, User logged);

        void Add(Siege siege);
        void Close(string id);
        void Sync(string id, Siege entity);
        void CheckOut(string id, string logged);
        void PushAds(string id, Advertising advertising);

        bool IsOwner(string id, string OwnerId);
    }
}
