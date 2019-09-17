using papuff.domain.Arguments.Security;
using papuff.domain.Arguments.Users;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace papuff.domain.Interfaces.Services.Core {
    public interface IServiceUser {

        Task<User> GetMe(string id);
        Task<User> GetByEmail(string email);
        Task<User> GetById(string id);

        Task<IEnumerable<User>> ListUsers();

        Task<AuthResponse> Authenticate(AuthRequest request);

        Task Create(UserRequest request, UserType type);
        Task Update(UserRequest request);
    }
}
