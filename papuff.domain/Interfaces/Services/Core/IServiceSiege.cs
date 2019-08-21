using papuff.domain.Arguments.Sieges;
using papuff.domain.Core.Sieges;
using papuff.domain.Core.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace papuff.domain.Interfaces.Services.Core {
    public interface IServiceSiege {

        Siege GetById(string id);
        List<Siege> ListSieges();

        Task Register(SiegeRequest request);
        Task Close(string id);

        Task ReceiveUser(string id, User logged);
        Task RemoveUser(string id, User logged);
    }
}
