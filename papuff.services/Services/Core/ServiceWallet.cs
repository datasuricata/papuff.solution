using papuff.domain.Core.Wallets;
using papuff.domain.Interfaces.Repositories;
using papuff.domain.Interfaces.Services.Core;
using papuff.services.Services.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace papuff.services.Services.Core {
    public class ServiceWallet : ServiceBase, IServiceWallet {

        private readonly IRepository<Wallet> _repoWallet;

        public ServiceWallet(IServiceProvider provider, IRepository<Wallet> repoWallet) : base(provider) {
            _repoWallet = repoWallet;
        }

        public async Task<Wallet> GetById(string id) {
            return await _repoWallet.ById(true, id);
        }

        public async Task<IEnumerable<Wallet>> GetByUser(string logged) {
            return await _repoWallet.ListBy(true, a => a.UserId == logged && !a.IsDeleted);
        }

        //public async Task Rec
    }
}