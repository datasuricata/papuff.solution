using papuff.domain.Core.Ads;
using papuff.domain.Core.Sieges;
using papuff.domain.Core.Users;
using System.Collections.Generic;

namespace papuff.domain.Interfaces.Services.Swap {
    public interface ISwapSiege {

        Siege GetById(string id);
        IEnumerable<Siege> ListSieges();

        void Add(Siege siege);
        void Close(string id);

        void PushAds(string id, Advertising advertising);
    }
}
