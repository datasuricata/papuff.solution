using papuff.domain.Arguments.Generals;
using papuff.domain.Arguments.Security;
using papuff.domain.Arguments.Users;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace papuff.domain.Interfaces.Services.Core {
    public interface IServiceUser {
        
        User GetMe(string id);
        User GetByEmail(string email);
        User GetById(string id);

        List<User> ListUsers();

        AuthResponse Authenticate(AuthRequest request);

        Task Register(UserRequest request, UserType type);
        Task General(GeneralRequest request);
        Task Address(AddressRequest request);
        Task Wallet(WalletRequest request);
    }
}
