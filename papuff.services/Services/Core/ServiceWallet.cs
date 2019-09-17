using papuff.domain.Arguments.Users;
using papuff.domain.Core.Users;
using papuff.domain.Interfaces.Repositories;
using papuff.domain.Interfaces.Services.Core;
using papuff.services.Services.Base;
using papuff.services.Validators.Core.Users;
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

        public async Task Wallet(WalletRequest request) {
            var current = await _repoWallet.By(false, u => u.Id == request.Id);

            if (current is null) {
                var wallet = new Wallet(request.Type, request.Agency, request.Account,
                    request.Document, request.DateDue, request.IsDefault, request.UserId);

                new WalletValidator().Validate(wallet);
                await _repoWallet.Register(wallet);
            } else {
                current.Update(request.Type, request.Agency, request.Account,
                    request.Document, request.DateDue, request.IsDefault);

                _repoWallet.Update(current);
            }
        }
    }
}