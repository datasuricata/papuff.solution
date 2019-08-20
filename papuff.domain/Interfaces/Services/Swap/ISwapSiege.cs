using papuff.domain.Core.Ads;
using papuff.domain.Core.Sieges;
using papuff.domain.Core.Users;
using System.Collections.Generic;

namespace papuff.domain.Interfaces.Services.Swap {
    public interface ISwapSiege {

        Siege GetById(string id);
        List<Siege> ListSieges();

        void Add(Siege siege);
        void Close(string id);
        void CloseAll();

        void ReceiveUser(string id, User user);
        void RemoveUser(string id, User user);

        void PushAds(string id, Advertising advertising);
    }
}
