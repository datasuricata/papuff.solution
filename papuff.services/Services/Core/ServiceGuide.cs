using papuff.domain.Arguments.Generals;
using papuff.domain.Arguments.Sieges;
using papuff.domain.Core.Companies;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Sieges;
using papuff.domain.Core.Wallets;
using papuff.domain.Interfaces.Repositories;
using papuff.domain.Interfaces.Services.Core;
using papuff.domain.Interfaces.Services.Swap;
using papuff.services.Services.Base;
using System;
using System.Threading.Tasks;

namespace papuff.services.Services.Core {
    public class ServiceGuide : ServiceBase, IServiceGuide {

        private readonly IRepository<Wallet> _repoWallet;
        private readonly IRepository<Company> _repoCompany;
        private readonly IRepository<Receipt> _repoReceipt;
        private readonly IServiceSiege _ss;

        public ServiceGuide(
            IRepository<Company> repoCompany,
            IRepository<Wallet> repoWallet,
            IRepository<Receipt> repoReceipt,
            IServiceSiege ss,
            IServiceProvider provider) : base(provider) {
            _repoWallet = repoWallet;
            _repoCompany = repoCompany;
            _repoReceipt = repoReceipt;
            _ss = ss;
        }

        public async Task Create(GuideRequest request) {

            // todo validate request

            var wallet = await _repoWallet.By(true, x => !x.IsDeleted && x.UserId == request.UserId);

            if (wallet is null) {
                wallet = new Wallet(request.UserId);
                await _repoWallet.Register(wallet);
            }

            await _repoReceipt.Register(new Receipt(request.Agency, request.Account, request.DateDue, wallet.Id));
            await _repoCompany.Register(new Company(request.Name, request.Email, request.SiteUri, request.CNPJ, null, request.Registration, DateTime.Now, request.UserId));

            await _ss.Create(new SiegeRequest {
                Description = request.Description, ImageUri = request.ImageUri, Latitude = 0, Longitude = 0, Seconds = 30, Visibility = request.Visibility,
                OperationIn = request.OperationIn, OperationTime = request.OperationTime, OwnerId = request.UserId, Range = request.Range, Title = request.Title,
            });
        }
    }
}
