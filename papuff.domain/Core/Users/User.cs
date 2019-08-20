using papuff.domain.Core.Base;
using papuff.domain.Core.Generals;
using System.Collections.Generic;

namespace papuff.domain.Core.Users {
    public class User : EntityBase {

        public User(string email, string password, string nick) {

            Email = email;
            Password = password;
            Nick = nick;
        }

        protected User() { }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Nick { get; set; }

        public Address Address { get; set; }
        public General General { get; set; }

        public List<Wallet> Wallets { get; set; } = new List<Wallet>();
        public List<Document> Documents { get; set; } = new List<Document>();
    }
}
