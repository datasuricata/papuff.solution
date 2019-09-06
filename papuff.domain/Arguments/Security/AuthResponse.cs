using papuff.domain.Core.Enums;
using papuff.domain.Core.Users;

namespace papuff.domain.Arguments.Security {
    public class AuthResponse {

        public string Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public UserType Type { get; set; }

        public static explicit operator AuthResponse(User v) {
            return v == null ? null : new AuthResponse {
                Id = v.Id,
                Email = v.Email,
                Name = v.General.Name,
                Type = v.Type,
            };
        }
    }
}
