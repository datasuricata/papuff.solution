using Microsoft.Extensions.Configuration;
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
    public class ServiceUser : ServiceBase, IServiceUser {
        #region - attributes -

        /// <summary>
        /// Use this to retrive JWT key from config
        /// </summary>
        private readonly IConfiguration _appConf;

        private readonly IRepository<User> _repoUser;
        private readonly IRepository<Document> _repoDocument;
        private readonly IRepository<General> _repoGeneral;
        private readonly IRepository<Address> _repoAddress;
        private readonly IRepository<Wallet> _repoWallet;

        #endregion

        #region - ctor -

        public ServiceUser(IServiceProvider provider,
        IConfiguration appConf,
        IRepository<User> repoUser,
        IRepository<General> repoGeneral,
        IRepository<Address> repoAddress,
        IRepository<Wallet> repoWallet,
        IRepository<Document> repoDocument) : base(provider) {
            _appConf = appConf;
            _repoUser = repoUser; 
            _repoDocument = repoDocument;
            _repoGeneral = repoGeneral;
            _repoAddress = repoAddress;
            _repoWallet = repoWallet;
        }

        #endregion

        #region - default -

        /// <summary>
        /// Use this to retrive user by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetMe(string id) {
            return await _repoUser.ById(false, id);
        }

        /// <summary>
        /// Use this to retrive user by login
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> GetByEmail(string email) {
            return await _repoUser.By(false, m => m.Email
                .Equals(email, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<User> GetById(string id) {
            return await _repoUser.ById(false, id, i => i.General, i => i.Documents);
        }

        public async Task<IEnumerable<User>> ListUsers() {
            return await _repoUser.ListBy(false, x => !x.IsDeleted);
        }

        #endregion

        #region - security -

        /// <summary>
        /// Use this to valid and create a jwt response from parameters request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AuthResponse> Authenticate(AuthRequest request) {
            var user = await _repoUser
                .By(true, u => u.Email.Equals(request.Login, StringComparison.InvariantCultureIgnoreCase) ||
                    u.Nick.Equals(request.Login, StringComparison.InvariantCultureIgnoreCase));

            _notify.When<ServiceUser>(user == null,
                "Usuário não encontrado.");

            _notify.When<ServiceUser>(user?.Password != request.Password,
                "Senha não confere, verifique e tente novamente.");

            //ValidEntity<SecurityValidator>(user);

            if (!_notify.IsValid) return null;

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
            _notify.When<ServiceUser>(
                _repoUser.Exist(u => u.Email.Equals(request.Email, StringComparison.InvariantCultureIgnoreCase)),
                "Já existe um registro para este e-mail.");

            _notify.When<ServiceUser>(
                _repoUser.Exist(u => u.Nick.Equals(request.Nick, StringComparison.InvariantCultureIgnoreCase)),
                "Este nick já esta em uso.");

            if (_notify.IsValid) {
                var user = new User(request.Email, request.Password.Encrypt(), request.Nick);
                user.SetType(type);

                _notify.Validate(user, new UserValidator());
                await _repoUser.Register(user);
            }
        }

        public async Task General(GeneralRequest request) {
            var current = await _repoGeneral.By(false, u => u.UserId == request.UserId);

            if (current is null) {
                var general = new General(request.BirthDate, request.Name,
                    request.Description, CurrentStage.Pending, request.UserId);

                new GeneralValidator().Validate(general);
                await _repoGeneral.Register(general);
            } else {
                current.Update(request.BirthDate, request.Name,
                    request.Description);
                _repoGeneral.Update(current);
            }
        }

        public async Task Address(AddressRequest request) {
            var current = await _repoAddress.By(false, u => u.UserId == request.OwnerId || u.CompanyId == request.OwnerId);

            if (current is null) {
                var address = new Address(request.Building, request.Number, request.Complement,
                    request.AddressLine, request.District, request.City, request.StateProvince,
                    request.Country, request.PostalCode, request.OwnerId, false);

                new AddressValidator().Validate(address);
                await _repoAddress.Register(address);
            } else {
                current.Update(request.Building, request.Number, request.Complement,
                    request.AddressLine, request.District, request.City, request.StateProvince,
                    request.Country, request.PostalCode);

                _repoAddress.Update(current);
            }
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

        public async Task Document(DocumentRequest request) {
            var current = await _repoDocument.By(false, u => u.Id == request.Id);

            if(current is null) {
                var document = new Document(request.Value, request.ImageUri, request.Type, request.UserId);
            }
        }

        #endregion
    }
}