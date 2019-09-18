using papuff.domain.Arguments.Users;
using papuff.domain.Core.Wallets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace papuff.domain.Interfaces.Services.Core {
    public interface IServiceWallet {
        Task<Wallet> GetById(string id);
        Task<IEnumerable<Wallet>> GetByUser(string logged);
        //Task Wallet(WalletRequest request);
    }
}