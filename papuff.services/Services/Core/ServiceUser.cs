﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using papuff.domain.Arguments.Generals;
using papuff.domain.Arguments.Security;
using papuff.domain.Arguments.Users;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Generals;
using papuff.domain.Core.Users;
using papuff.domain.Interfaces.Repositories;
using papuff.domain.Interfaces.Services.Core;
using papuff.domain.Security;
using papuff.services.Services.Base;
using papuff.services.Validators.Core.Generals;
using papuff.services.Validators.Core.Users;
using papuff.services.Validators.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace papuff.services.Services.Core {
    public class ServiceUser : ServiceApp<User>, IServiceUser {
        #region - attributes -

        /// <summary>
        /// Use this to retrive JWT key from config
        /// </summary>
        private readonly IConfiguration _appConf;

        private readonly IRepository<Document> _repositoryDocument;
        private readonly IRepository<General> _repositoryGeneral;
        private readonly IRepository<Address> _repositoryAddress;
        private readonly IRepository<Wallet> _repositoryWallet;

        #endregion

        #region - ctor -

        public ServiceUser(IServiceProvider provider,
        IConfiguration appConf,
        IRepository<General> repositoryGeneral,
        IRepository<Address> repositoryAddress,
        IRepository<Wallet> repositoryWallet,
        IRepository<Document> repositoryDocument) : base(provider) {
            _appConf = appConf;
            _repositoryDocument = repositoryDocument;
            _repositoryGeneral = repositoryGeneral;
            _repositoryAddress = repositoryAddress;
            _repositoryWallet = repositoryWallet;
        }

        #endregion

        #region - default -

        /// <summary>
        /// Use this to retrive user by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetMe(string id) {
            return _repository.GetById(false, id);
        }

        /// <summary>
        /// Use this to retrive user by login
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> GetByEmail(string email) {
            return await _repository.GetByAsync(false, m => m.Email
                .Equals(email, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<User> GetById(string id) {
            return await _repository.GetByIdAsync(false, id, i => i.General, i => i.Documents);
        }

        public async Task<IEnumerable<User>> ListUsers() {
            return await _repository.ListByAsync(false, x => !x.IsDeleted);
        }

        #endregion

        #region - security -

        /// <summary>
        /// Use this to valid and create a jwt response from parameters request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AuthResponse Authenticate(AuthRequest request) {
            var user = _repository
                .GetBy(true, u => u.Email
                    .Equals(request.Email, StringComparison.InvariantCultureIgnoreCase));

            Notifier.When<ServiceUser>(user == null,
                "Usuário não encontrado.");

            Notifier.When<ServiceUser>(user?.Password != request.Password,
                "Senha não confere, verifique e tente novamente.");

            //ValidEntity<SecurityValidator>(user);

            if (!Notifier.IsValid) return null;

            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appConf["SecurityKey"]);

            var payload = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = DateTime.UtcNow,
            };

            var token = handler.CreateToken(payload);
            return ((AuthResponse)user).InjectToken(handler.WriteToken(token));
        }

        #endregion

        #region - register -

        public async Task Register(UserRequest request, UserType type) {
            Notifier.When<ServiceUser>(
                _repository.Exist(u => u.Email.Equals(request.Email, StringComparison.InvariantCultureIgnoreCase)),
                "Já existe um registro para este e-mail.");

            Notifier.When<ServiceUser>(
                _repository.Exist(u => u.Nick.Equals(request.Nick, StringComparison.InvariantCultureIgnoreCase)),
                "Este nick já esta em uso.");

            if (Notifier.IsValid) {
                var user = new User(request.Email, request.Password.Encrypt(), request.Nick);
                user.SetType(type);

                ValidEntity<UserValidator>(user);
                await _repository.RegisterAsync(user);
            }
        }

        public async Task General(GeneralRequest request) {
            var current = _repositoryGeneral.GetBy(false, u => u.UserId == request.UserId);

            if (current is null) {
                var general = new General(request.BirthDate, request.Name,
                    request.Description, CurrentStage.Pending, request.UserId);

                new GeneralValidator().Validate(general);
                await _repositoryGeneral.RegisterAsync(general);
            } else {
                current.Update(request.BirthDate, request.Name,
                    request.Description);
                _repositoryGeneral.Update(current);
            }
        }

        public async Task Address(AddressRequest request) {
            var current = _repositoryAddress.GetBy(false, u => u.UserId == request.OwnerId || u.CompanyId == request.OwnerId);

            if (current is null) {
                var address = new Address(request.Building, request.Number, request.Complement,
                    request.AddressLine, request.District, request.City, request.StateProvince,
                    request.Country, request.PostalCode, request.OwnerId, false);

                new AddressValidator().Validate(address);
                await _repositoryAddress.RegisterAsync(address);
            } else {
                current.Update(request.Building, request.Number, request.Complement,
                    request.AddressLine, request.District, request.City, request.StateProvince,
                    request.Country, request.PostalCode);

                _repositoryAddress.Update(current);
            }
        }

        public async Task Wallet(WalletRequest request) {
            var current = _repositoryWallet.GetBy(false, u => u.Id == request.Id);

            if (current is null) {
                var wallet = new Wallet(request.Type, request.Agency, request.Account,
                    request.Document, request.DateDue, request.IsDefault, request.UserId);

                new WalletValidator().Validate(wallet);
                await _repositoryWallet.RegisterAsync(wallet);
            } else {
                current.Update(request.Type, request.Agency, request.Account,
                    request.Document, request.DateDue, request.IsDefault);

                _repositoryWallet.Update(current);
            }
        }

        public async Task Document(DocumentRequest request) {
            var current = _repositoryDocument.GetByAsync(false, u => u.Id == request.Id);

            if(current is null) {
                var document = new Document(request.Value, request.ImageUri, request.Type, request.UserId);
                new DocumentValidator
            }
        }

        #endregion
    }
}