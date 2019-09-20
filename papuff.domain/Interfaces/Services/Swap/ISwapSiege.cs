using papuff.domain.Core.Sieges;
using papuff.domain.Core.Users;
using System.Collections.Generic;

namespace papuff.domain.Interfaces.Services.Swap {
    public interface ISwapSiege {

        Siege GetById(string id);
        IEnumerable<Siege> ListSieges();
        IEnumerable<Siege> ListAvaiables();
        IEnumerable<Siege> CheckIn(IEnumerable<Siege> sieges, User logged);

        void AddSiege(Siege siege);
        void Close(string id);
        void Sync(string id, Siege entity);
        void CheckOut(string id, string logged);
        void AddAds(string id, Advertising advertising);
        void AddTickets(string id, List<Ticket> tickets);

        bool IsOwner(string id, string OwnerId);
    }
}