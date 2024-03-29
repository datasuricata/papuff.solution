﻿using papuff.domain.Core.Base;
using papuff.domain.Core.Companies;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Generals;
using papuff.domain.Core.Wallets;
using System.Collections.Generic;

namespace papuff.domain.Core.Users {
    public class User : EntityBase {

        public User(string email, string password, string nick) {
            Email = email;
            Password = password;
            Nick = nick;
        }

        protected User() { }

        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Nick { get; private set; }

        public UserType Type { get; private set; }

        public Address Address { get; set; }
        public General General { get; set; }
        public Wallet Wallet { get; set; } 

        public List<Company> Companies { get; set; } = new List<Company>();
        public List<Document> Documents { get; set; } = new List<Document>();

        public void Update(string email, string password, string nick) {
            Email = email;
            Password = password ?? Password;
            Nick = nick ?? Nick;
        }

        public void SetType(UserType type) {
            Type = type;
        }

        public void ValidateDocs() {
            if (Documents.TrueForAll(a => a.Aproved))
                General.SetStage(CurrentStage.Aproved);

            if (Documents.TrueForAll(a => !a.Aproved))
                General.SetStage(CurrentStage.Recused);
        }
    }
}