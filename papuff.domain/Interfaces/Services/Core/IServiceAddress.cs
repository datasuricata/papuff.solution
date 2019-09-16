using papuff.domain.Arguments.Users;
using papuff.domain.Core.Users;
using System.Threading.Tasks;

namespace papuff.domain.Interfaces.Services.Core {
    public interface IServiceAddress {
        Task<Address> GetById(string id);
        Task<Address> GetByUser(string logged);
        Task Address(AddressRequest request);
    }
}
