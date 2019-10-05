using papuff.domain.Arguments.Users;
using papuff.domain.Core.Wallets;
using System.Threading.Tasks;

namespace papuff.domain.Interfaces.Services.Core {
    public interface IServiceWallet {

        Task<Wallet> GetById(string id);
        Task<Wallet> GetByUser(string logged);

        Task Wallet(string userId);
        Task Receipt(ReceiptRequest param);

        Task Payment(PaymentRequest param);
        Task Payment(string id);
    }
}