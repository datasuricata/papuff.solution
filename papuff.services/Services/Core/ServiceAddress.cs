using papuff.domain.Arguments.Users;
using papuff.domain.Core.Users;
using papuff.domain.Interfaces.Repositories;
using papuff.domain.Interfaces.Services.Core;
using papuff.services.Services.Base;
using papuff.services.Validators.Core.Users;
using System;
using System.Threading.Tasks;

namespace papuff.services.Services.Core {
    public class ServiceAddress : ServiceBase, IServiceAddress {

        private readonly IRepository<Address> _repoAddress;

        public ServiceAddress(IServiceProvider provider, IRepository<Address> repoAddress) : base(provider) {
            _repoAddress = repoAddress;
        }

        public async Task<Address> GetById(string id) {
            return await _repoAddress.ById(true, id);
        }

        public async Task<Address> GetByUser(string logged) {
            return await _repoAddress.By(true, a => a.UserId == logged);
        }

        public async Task Address(AddressRequest request) {
            var current = await _repoAddress.By(false, u => u.UserId == request.UserId || u.CompanyId == request.UserId);

            if (current is null) {
                var address = new Address(request.Building, request.Number, request.Complement,
                    request.AddressLine, request.District, request.City, request.StateProvince,
                    request.Country, request.PostalCode, request.UserId, false);

                new AddressValidator().Validate(address);
                await _repoAddress.Register(address);
            } else {
                current.Update(request.Building, request.Number, request.Complement,
                    request.AddressLine, request.District, request.City, request.StateProvince,
                    request.Country, request.PostalCode);

                _repoAddress.Update(current);
            }
        }
    }
}