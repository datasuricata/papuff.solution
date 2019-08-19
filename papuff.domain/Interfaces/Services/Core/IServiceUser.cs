using papuff.domain.Arguments.Security;
using papuff.domain.Core.Users;
using System.Collections.Generic;

namespace papuff.domain.Interfaces.Services.Core {
    public interface IServiceUser {
        
        // common
        User GetMe(string id);
        User GetByEmail(string email);
        User GetById(string id);
        List<User> ListUsers();

        AuthResponse Authenticate(AuthRequest request);
    }
}
