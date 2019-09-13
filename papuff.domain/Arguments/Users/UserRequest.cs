using Newtonsoft.Json;
using papuff.domain.Core.Enums;

namespace papuff.domain.Arguments.Users {
    public class UserRequest {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string Nick { get; set; }
        public UserType Type { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}