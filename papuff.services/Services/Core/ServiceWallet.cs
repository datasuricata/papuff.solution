﻿using papuff.domain.Arguments.Users;
using papuff.domain.Core.Wallets;
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
        private readonly IRepository<Receipt> _repoReceipt;
        private readonly IRepository<Payment> _repoPayment;

        public ServiceWallet(IServiceProvider provider,
            IRepository<Receipt> repoReceipt,
            IRepository<Payment> repoPayment,
            IRepository<Wallet> repoWallet) : base(provider) {
            _repoWallet = repoWallet;
            _repoPayment = repoPayment;
            _repoReceipt = repoReceipt;
        }

        public async Task<Wallet> GetById(string id) {
            return await _repoWallet.ById(true, id,
                i => i.Receipt,
                i => i.Payments);
        }

        public async Task<Wallet> GetByUser(string userId) {
            return await _repoWallet.By(true, a => a.UserId == userId, 
                i => i.Receipt, 
                i => i.Payments);
        }

        public async Task Wallet(string userId) {
            var current = await _repoWallet.By(false, w => w.UserId == userId);

            if (current is null) {
                var wallet = new Wallet(userId);
                _notify.Validate(wallet, new WalletValidator());

                await _repoWallet.Register(wallet);
            }
        }

        public async Task Receipt(ReceiptRequest param) {
            var current = await _repoReceipt.By(false, r => r.WalletId == param.WalletId);

            if(current is null) {
                var receipt = new Receipt(param.Agency, param.Account, 
                    param.DateDue, param.WalletId);

                _notify.Validate(receipt, new ReceiptValidator());
                await _repoReceipt.Register(receipt);

            } else {
                current.Update(param.Agency, param.Account, param.DateDue);
                _repoReceipt.Update(current);
            }
        }

        #region - payment -

        public async Task Payment(PaymentRequest param) {
            var payment = new Payment(param.Card, param.Expiration, param.Code,
                param.DateDue, param.Document, param.IsDefault, param.Type, param.WalletId);

            _notify.Validate(payment, new PaymentValidator());
            await _repoPayment.Register(payment);
        }

        /// <summary>
        /// delete 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Payment(string id) {
            var current = await _repoPayment.ById(false, id);
            current.IsDeleted = true;

            _repoPayment.Update(current);
        }

        #endregion
    }
}