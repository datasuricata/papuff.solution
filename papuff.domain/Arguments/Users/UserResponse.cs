using System;
using papuff.domain.Core.Users;
using papuff.domain.Helpers;

namespace papuff.domain.Arguments.Users {
    public class UserResponse {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nick { get; set; }
        public string Type { get; set; }

        public static explicit operator UserResponse(User v) {
            return v == null ? null : new UserResponse {
                Email = v.Email,
                Nick = v.Nick,
                Type = v.Type.EnumDisplay(),
                Id = v.Id,
            };
        }
    }
}
