using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using papuff.domain.Arguments.Security;
using papuff.domain.Arguments.Users;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Generals;
using papuff.domain.Core.Users;
using papuff.domain.Core.Wallets;
using papuff.domain.Interfaces.Repositories;
using papuff.domain.Interfaces.Services.Core;
using papuff.domain.Security;
using papuff.services.Services.Base;
using papuff.services.Validators.Core.Generals;
using papuff.services.Validators.Core.Users;

namespace papuff.services.Services.Core {
    public class ServiceUser : ServiceBase, IServiceUser {
        #region - attributes -

        /// <summary>
        /// Use this to retrive JWT key from config
        /// </summary>
        private readonly IConfiguration _appConf;
        private readonly IRepository<User> _repoUser;

        #endregion

        #region - ctor -

        public ServiceUser (IServiceProvider provider,
            IConfiguration appConf,
            IRepository<User> repoUser) : base (provider) {
            _appConf = appConf;
            _repoUser = repoUser;
        }

        #endregion

        #region - default -

        /// <summary>
        /// Use this to retrive user by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetMe (string id) {
            return await _repoUser.ById (true, id);
        }

        /// <summary>
        /// Use this to retrive user by login
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> GetByEmail (string email) {
            return await _repoUser.By (true, m => m.Email
                .Equals (email, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<User> GetById (string id) {
            return await _repoUser.ById (true, id);
        }

        public async Task<IEnumerable<User>> ListUsers () {
            return await _repoUser.ListBy (true, x => !x.IsDeleted);
        }

        #endregion

        #region - security -

        /// <summary>
        /// Use this to valid and create a jwt response from parameters request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AuthResponse> Authenticate (AuthRequest request) {
            var user = await _repoUser
                .By (true, u => u.Email.Equals (request.Login, StringComparison.InvariantCultureIgnoreCase) ||
                    u.Nick.Equals (request.Login, StringComparison.InvariantCultureIgnoreCase), i => i.General);

            _notify.When<ServiceUser> (user == null,
                "Usuário não encontrado.");

            _notify.When<ServiceUser> (user?.Password != request.Password,
                "Senha não confere, verifique e tente novamente.");

            if (!_notify.IsValid) return null;

            var handler = new JwtSecurityTokenHandler ();
            var key = Encoding.ASCII.GetBytes (_appConf["SecurityKey"]);

            var payload = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (new [] {
                new Claim (ClaimTypes.NameIdentifier, user.Id),
                new Claim (ClaimTypes.Role, user.Type.ToString ()),
                }),
                Expires = DateTime.UtcNow.AddHours (3),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (key),
                SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = DateTime.UtcNow,
            };

            var token = handler.CreateToken (payload);
            return ((AuthResponse) user).InjectToken (handler.WriteToken (token));
        }

        #endregion

        public async Task Create (UserRequest request, UserType type) {
            _notify.When<ServiceUser> (
                _repoUser.Exist (u => u.Email.Equals (request.Email, StringComparison.InvariantCultureIgnoreCase)),
                "Já existe um registro para este e-mail.");

            _notify.When<ServiceUser> (
                _repoUser.Exist (u => u.Nick.Equals (request.Nick, StringComparison.InvariantCultureIgnoreCase)),
                "Este nick já esta em uso.");

            if (_notify.IsValid) {
                var user = new User (request.Email, request.Password.Encrypt (), request.Nick);
                user.SetType (type);

                _notify.Validate (user, new UserValidator ());
                await _repoUser.Register (user);
            }
        }

        public async Task Single (RegisterRequest request) {
            #region - validator -
            
            _notify.When<ServiceUser> (request.Password != request.ConfirmPassword,
                "Senhas informadas estão diferentes.");

            _notify.When<ServiceUser> (
                _repoUser.Exist (u => u.Email.Equals (request.Email, StringComparison.InvariantCultureIgnoreCase)),
                "Já existe um registro para este e-mail.");

            _notify.When<ServiceUser> (
                _repoUser.Exist (u => u.Nick.Equals (request.Nick, StringComparison.InvariantCultureIgnoreCase)),
                "Este nick já esta em uso.");

            #endregion

            var user = new User (request.Email, request.Password.Encrypt (), request.Nick);
            user.SetType (UserType.Enterprise);
            _notify.Validate (user, new UserValidator ());

            var general = new General(DateTime.Now, request.Name, null, CurrentStage.Pending, user.Id);
            _notify.Validate(general, new GeneralValidator());
            user.General = general;

            var address = new Address (request.Address.Building, request.Address.Number, request.Address.Complement,
                request.Address.AddressLine, request.Address.District, request.Address.City, request.Address.StateProvince,
                request.Address.Country, request.Address.PostalCode, user.Id, request.Address.Building == BuildingType.Commercial);
            _notify.Validate (address, new AddressValidator ());
            user.Address = address;

            await _repoUser.Register (user);
        }

        public async Task Update (UserRequest request) {

            var id = request.Id ?? request.UserId;
            var user = await _repoUser.ById (false, id);

            _notify.When<ServiceUser> (user == null,
                "Usuário não encontrado");

            if (!string.IsNullOrEmpty (request.Password)) {
                _notify.When<ServiceUser> (user.Password != request.Password,
                    "Senha inválida");

                user.Update (request.Email, request.NewPassword, request.Nick);
            }

            _repoUser.Update (user);
        }
    }
}