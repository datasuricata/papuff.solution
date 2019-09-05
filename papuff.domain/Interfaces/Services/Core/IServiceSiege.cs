using papuff.domain.Arguments.Companies;
using papuff.domain.Arguments.Sieges;
using papuff.domain.Core.Sieges;
using papuff.domain.Core.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace papuff.domain.Interfaces.Services.Core {
    public interface IServiceSiege {

        Siege GetById(string id);

        IEnumerable<Siege> ListSieges();
        IEnumerable<Siege> ReceiveEntry(LocationRequest request, User logged);

        Task Register(SiegeRequest request);
        Task Close(string id, string logged);

        void ReceiveAds(AdsRequest request);
    }
}
